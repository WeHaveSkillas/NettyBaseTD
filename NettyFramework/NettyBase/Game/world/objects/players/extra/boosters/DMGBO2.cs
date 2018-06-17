using System;

namespace NettyBase.Game.world.objects.players.extra.boosters
{
    class DMGBO2 : Booster
    {
        public DMGBO2(Player player, DateTime finishTime) : base(player, finishTime, Boosters.DMG_B02, Types.DAMAGE)
        {
        }

        private void CheckForBoost(Player player, bool isRemove)
        {
            Booster boost = null;
            if (isRemove) player?.InheritedBoosters.TryRemove(Player, out boost);
            else if (player != null && !player.InheritedBoosters.ContainsKey(Player))  player.InheritedBoosters.TryAdd(Player, this);
        }

        public override double GetBoost()
        {
            return 0.1;
        }

        public override double GetSharedBoost()
        {
            return 0.05;
        }
    }
}
