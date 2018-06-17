﻿using System.Linq;
using NettyBase.Game.world.objects.map.gg;

namespace NettyBase.Game.world.objects.map.objects.jumpgates
{
    class LowPortal : Jumpgate
    {
        public LowPortal(int id, Vector pos, Spacemap map, int vw) : base(id, Faction.NONE, pos, map, new Vector(1000, 11800), 200, true, 0, 0, 34)
        {
            DestinationVirtualWorldId = vw;
        }

        public override void click(Character character)
        {
            var player = character as Player;
            if (player == null) return;
            if (player.Group == null)
            {
                //TODO Send message player not in group
                return;
            }

            // If any other group member has the gate
            var groupMemberWithGateInitiated =
                player.Group.Members.FirstOrDefault(x => x.Value.OwnedGates.Any(y => y.Value is LowGate));

            if (groupMemberWithGateInitiated.Value != null)
            {
                var low = groupMemberWithGateInitiated.Value.OwnedGates.FirstOrDefault(x => x.Value is LowGate);
                if (low.Value.VWID != 0 && low.Value.VirtualMap != null)
                {
                    player.Controller.Miscs.Jump(low.Value.Spacemap.Id, Destination, Id, low.Value.VWID);
                    low.Value.PendingPlayers.TryAdd(player.Id, player);
                }
            }
            else
            {
                var low = new LowGate(0, World.StorageManager.Spacemaps[200]);
                player.CreateGalaxyGate(low);
                low.InitiateVirtualWorld();
                player.Controller.Miscs.Jump(low.Spacemap.Id, Destination, Id, low.VWID);
                low.PendingPlayers.TryAdd(player.Id, player);
            }
        }
    }
}
