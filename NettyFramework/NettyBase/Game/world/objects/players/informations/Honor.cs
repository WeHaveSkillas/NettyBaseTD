using System;

namespace NettyBase.Game.world.objects.players.informations
{
    class Honor : BaseInfo
    {
        public Honor(Player player) : base(player)
        {
            SqlName = "HONOR";
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
