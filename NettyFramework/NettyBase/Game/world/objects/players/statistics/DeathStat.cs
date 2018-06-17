using System;

namespace NettyBase.Game.world.objects.players.statistics
{
    class DeathStat
    {
        public int KillerId { get; }

        public string KillerName { get; }

        public Ship KillerShip { get; }

        public DateTime DeathTime { get; }

        public DeathStat(int killerId, string killerName, Ship killerShip, DateTime deathTime)
        {
            KillerId = killerId;
            KillerName = killerName;
            KillerShip = killerShip;
            DeathTime = deathTime;
        }
    }
}
