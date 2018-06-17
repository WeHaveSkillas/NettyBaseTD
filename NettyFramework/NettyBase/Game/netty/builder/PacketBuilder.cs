using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NettyBase.Game.world;
using NettyBase.Networking.game_server;
using NettyFramework.Commands;

namespace NettyBase.Game.netty.builder
{
    class PacketBuilder
    {
        private GameSession _session;

        public PacketBuilder(GameSession gameSession)
        {
            _session = gameSession;
        }

        public void execute(byte[] bytes)
        {
            _session?.Client?.Send(bytes);
        }

        #region commands

        #region Ship

        public void ShipInitializationCommand()
        {
            var player = _session.Player;
            execute(NettyFramework.Commands.ShipInitializationCommand.write(player.Id, player.Name, player.Hangar.Ship.LootId, player.Speed, player.CurrentShield, player.MaxShield, player.CurrentHealth,
                player.MaxHealth, 0, 0, player.CurrentNanoHull, player.MaxNanoHull, player.Position.X, player.Position.Y, player.Spacemap.Id, (int) player.FactionId, player.Clan.Id, 15, player.Information.Premium.Active, player.Information.Experience.Value, player.Information.Honor.Value, player.Information.Level.Id, player.Information.Credits.Value, player.Information.Uridium.Value,
                0, (int) player.RankId, player.Clan.Tag, 100, false, player.Invisible, true, new List<VisualModifierCommand>()));
        }
        #endregion
        #endregion

    }
}
