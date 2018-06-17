using System;

namespace NettyBase.Game.world.objects.players.informations
{
    class Exp : BaseInfo
    {
        public Exp(Player player) : base(player)
        {
            SqlName = "EXP";
        }

        public override void Refresh()
        {
            World.DatabaseManager.LoadInfo(Player, this);
            Value = SyncedValue;
        }

        public override void Add(double amount)
        {
            World.DatabaseManager.UpdateInfo(Player, this, amount);
            Value = SyncedValue;
        }

        public override void Remove(double amount)
        {
            World.DatabaseManager.UpdateInfo(Player, this, -amount);
            Value = SyncedValue;
        }

        public override void Set(double value)
        {
            throw new NotImplementedException();
        }
    }
}
