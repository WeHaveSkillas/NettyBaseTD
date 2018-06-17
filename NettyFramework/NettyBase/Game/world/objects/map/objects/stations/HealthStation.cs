using System;
using System.Collections.Generic;

namespace NettyBase.Game.world.objects.map.objects.stations
{
    class HealthStation : Station
    {
        public List<Player> PlayersInRangeOfStation = new List<Player>();

        public HealthStation(int id, Vector pos, Spacemap map) : base(id, new List<StationModule>(), Faction.NONE, pos, map)
        {
        }

        public override void Tick()
        {
            HealPlayersInRangeOfStation();
        }

        public override void execute(Character character)
        {
            var player = character as Player;
            if (player != null && !PlayersInRangeOfStation.Contains(player))
            {
                PlayersInRangeOfStation.Add(player);
            }
        }

        private void HealPlayersInRangeOfStation()
        {
            foreach (var player in PlayersInRangeOfStation)
            {
                var session = player.GetGameSession();
                if (session == null) continue;
                if (player.CurrentHealth != player.MaxHealth && player.LastCombatTime.AddSeconds(10) <= DateTime.Now)
                {
                    var heal = player.MaxHealth / 10;
                    if (player.CurrentHealth + heal > player.MaxHealth) heal = player.MaxHealth - player.CurrentHealth;
                    player.Controller.Heal.Execute(heal, Id);
                    //Packet.Builder.LegacyModule(session, "0|CSS|1");
                    //todo: fix
                }
                else
                {
                    //Packet.Builder.LegacyModule(session, "0|CSS|0");
                }
            }
        }
    }
}
