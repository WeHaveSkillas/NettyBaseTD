﻿using System;

namespace NettyBase.Game.world.objects.players
{
    class Ammunition : PlayerBaseClass
    {
        public string LootId { get; set; }
        private int Amount { get; set; }
        private int SyncedAmount { get; set; }

        public DateTime LastSynchronizationTime = new DateTime();

        public Ammunition(Player player, string lootId, int amount) : base(player)
        {
            LootId = lootId;
            Amount = amount;
            SyncedAmount = amount;
            LastSynchronizationTime = DateTime.Now;
        }

        public int Get()
        {
            SyncCheck();
            return Amount;
        }

        public int Shoot()
        {
            SyncCheck();
            int fireCount;
            if (LootId.Contains("ammunition_laser"))
                fireCount = Player.Equipment.LaserCount();
            else fireCount = 1;
            var newAmount = Amount - fireCount;
            
            if (newAmount < 0)
            {
                return 0;
            }
            Amount = newAmount;
//            Packet.Builder.AmmunitionCountUpdateCommand(World.StorageManager.GetGameSession(Player.Id), LootId, Amount);
            return fireCount;
        }

        public void Add(int amount)
        {
            Amount += amount;
            SyncCheck(true);
            //Packet.Builder.AmmunitionCountUpdateCommand(World.StorageManager.GetGameSession(Player.Id), LootId, Amount);
        }                //todo: fix


        private void SyncCheck(bool force = false)
        {
            if (LastSynchronizationTime.AddSeconds(15) < DateTime.Now || force)
            {
                SyncedAmount = World.DatabaseManager.UpdateAmmo(this, SyncedAmount - Amount);
                Amount = SyncedAmount;
                LastSynchronizationTime = DateTime.Now;
            }
        }

        public static void ForceSync(Player player)
        {
            foreach (var ammo in player.Information.Ammunitions)
            {
                ammo.Value.SyncCheck(true);
                //Packet.Builder.AmmunitionCountUpdateCommand(World.StorageManager.GetGameSession(player.Id), ammo.Key,
                //    ammo.Value.Amount);
            }
        }
    }
}
