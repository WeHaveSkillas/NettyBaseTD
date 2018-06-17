using System;
using NettyBase.Game.controllers;
using NettyBase.Game.world.objects.players;
using NettyBase.Game.world.objects.players.events;

namespace NettyBase.Game.world.objects.events
{
    class GameEvent
    {
        /// <summary>
        /// Event ID
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Event name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Event Controller
        /// </summary>
        public EventController Controller { get; set; }

        public EventTypes EventType { get; set; }

        public bool Active { get; set; }

        public bool Open => true; // TODO Change for some events which are opening only based on players who're active

        public GameEvent(int id, string name, EventTypes eventType, bool active)
        {
            Id = id;
            Name = name;
            EventType = eventType;
            Active = active;
        }

        public void Start()
        {
            Active = true;
            World.DatabaseManager.UpdateServerEvent(Id, Active);
            foreach (var gameSession in World.StorageManager.GameSessions.Values)
            {
                CreatePlayerEvent(gameSession.Player);
                //todo: fix
                //Packet.Builder.LegacyModule(gameSession, $"0|A|STD|{Name} have started!");
            }
            Console.WriteLine($"{EventType} started.");
        }

        public void CreatePlayerEvent(Player player)
        {
            PlayerEvent playerEvent = null;
            if (!player.EventsPraticipating.ContainsKey(Id))
            {
                if (EventType == EventTypes.SCOREMAGEDDON)
                {
                    playerEvent = new ScoreMageddon(player, Id);
                }
                if (playerEvent != null)
                {
                    player.EventsPraticipating.TryAdd(Id, playerEvent);
                }
            }
            else playerEvent = player.EventsPraticipating[Id];
            playerEvent.Start();
        }

        public void End()
        {
            foreach (var gameSession in World.StorageManager.GameSessions.Values)
            {
                gameSession.Player.EventsPraticipating[Id].End();
            }
            World.StorageManager.Events.Remove(Id);
        }
    }
}
