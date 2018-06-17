﻿using System;

namespace NettyBase.Game.world.objects.characters
{
    class RocketLauncher
    {
        private Character Character;

        public int CurrentLoad { get; set; }

        public string LoadLootId { get; set; }

        public int[] Launchers { get; set; }

        private bool ReloadingActive = false;

        public RocketLauncher(Character character, int[] launchers = null)
        {
            Character = character;
            LoadLootId = "ammunition_rocketlauncher_eco-10";
            Launchers = launchers;
        }

        public void Tick()
        {
            if (Launchers != null)
            {
                if (ReloadingActive) Reload();
                GetCPU();
            }
        }

        public int GetMaxLoad()
        {
            int maxLoad = 0;
            foreach (var launcher in Launchers)
            {
                if (launcher == 2)
                    maxLoad += 5;
                else maxLoad += 3;
            }
            return maxLoad;
        }

        private void GetCPU()
        {
            // AUTO RL
            if (LastShoot.AddSeconds(3) < DateTime.Now)
                ReloadingActive = true;
        }

        private DateTime LastReloadTime = new DateTime();
        public void Reload()
        {
            if (LastReloadTime.AddSeconds(1) > DateTime.Now) return;
            if (CurrentLoad == GetMaxLoad())
            {
                ReloadingActive = false;
                return;
            }

            var player = Character as Player;
            if (player != null)
            {
                if (player.Information.Ammunitions[LoadLootId].Get() > CurrentLoad)
                {
                    CurrentLoad++;
                    //Packet.Builder.HellstormStatusCommand(World.StorageManager.GetGameSession(player.Id));
                }
                else ReloadingActive = false;
            }
            else CurrentLoad++;

            ReloadingActive = true;

            LastReloadTime = DateTime.Now;
        }

        private DateTime LastShoot= new DateTime();
        public void Shoot()
        {
            var player = Character as Player;
            if (player != null)
            {
                for (var i = 0; i <= CurrentLoad; i++)
                {
                    player.Information.Ammunitions[LoadLootId].Shoot();
                }
                ReloadingActive = false;
                LastShoot = DateTime.Now;
            }
        }

        public void ChangeLoad(string lootId)
        {
            ReloadingActive = false;
            CurrentLoad = 0;
            LoadLootId = lootId;
            var player = Character as Player;
            if (player != null)
            {
                //todo: fix
                //Packet.Builder.HellstormStatusCommand(World.StorageManager.GetGameSession(player.Id));
            }
        }
    }
}
