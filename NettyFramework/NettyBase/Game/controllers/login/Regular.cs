using System.Collections.Generic;
using NettyBase.Game.world;
using NettyBase.Game.world.objects;
using NettyBase.Game.world.objects.players.quests;

namespace NettyBase.Game.controllers.login
{
    class Regular : ILogin
    {
        public Regular(GameSession gameSession) : base(gameSession)
        {
        }

        public override void Execute()
        {
            InitiateEvents();
            SendSettings();
            Spawn();
            SendLegacy();
        }

        public void Spawn()
        {
            var player = GameSession.Player;
            if (!player.Spacemap.Entities.ContainsKey(player.Id))
            {
                player.Spacemap.AddEntity(player);
            }
            else
            {
                player.Range.Clear();
                player.Storage.Clean();
            }

            //TODO: Fix
            //Packet.Builder.ShipInitializationCommand(GameSession);
            //Packet.Builder.DronesCommand(GameSession, GameSession.Player);
        }
    }
}
