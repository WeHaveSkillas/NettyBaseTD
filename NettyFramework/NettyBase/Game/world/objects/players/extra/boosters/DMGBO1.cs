using System;

namespace NettyBase.Game.world.objects.players.extra.boosters
{
    class DMGBO1 : Booster
    {
        public DMGBO1(Player player, DateTime finishTime) : base(player, finishTime, Boosters.DMG_B01, Types.DAMAGE) { }

        public override double GetBoost()
        {
            return 1.1;
        }

        public override double GetSharedBoost()
        {
            return 0;
        }
    }
}
