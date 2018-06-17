﻿using System;
using System.Linq;
using NettyBase.Game.controllers.implementable;
using NettyBase.Game.controllers.player.structs;
using NettyBase.Game.world;
using NettyBase.Game.world.objects;
using NettyBase.Game.world.objects.characters.cooldowns;
using NettyBase.Game.world.objects.map.objects;
using NettyBase.Game.world.objects.players.settings.slotbars;

namespace NettyBase.Game.controllers.player
{
    class Misc : IChecker
    {
        // TODO: Make every function return 0 / 1 & stuff to be handled by the response.

        private PlayerController baseController;

        private jClass JClass { get; set; }

        public Misc(PlayerController controller)
        {
            baseController = controller;
            JClass = new jClass(controller);
        }

        public bool LoggingOut = false;
        private DateTime LogoutStartTime = new DateTime();

        public void Logout(bool start = false)
        {
            if (start)
            {
                LoggingOut = true;
                LogoutStartTime = DateTime.Now;
                return;
            }

            if (!LoggingOut) return;

            var gameSession = baseController.Player.GetGameSession();

            if (baseController.Attack.Attacking || baseController.Attack.GetActiveAttackers()?.Count > 0)
            {
                AbortLogout();
                return;
            }

            if (gameSession.Player.Information.Premium.Active && LogoutStartTime.AddSeconds(5) < DateTime.Now
            || LogoutStartTime.AddSeconds(20) < DateTime.Now)
            {
                //todo:fix
//                Packet.Builder.LogoutCommand(gameSession);
                gameSession.Disconnect(GameSession.DisconnectionType.NORMAL);
                LoggingOut = false;
            }

        }

        public void AbortLogout()
        {
            var gameSession = baseController.Player.GetGameSession();
            LoggingOut = false;
            //todo:fix
            //Packet.Builder.LegacyModule(gameSession, "0|t");
        }

        private DateTime lastDamagedTime = new DateTime();

        public void RadiationZone()
        {
            if (baseController.Player.State.InRadiationArea)
            {
                if(lastDamagedTime.AddSeconds(1) < DateTime.Now)
                {
                    var radiationDamage = (DateTime.Now - baseController.Player.State.RadiationEntryTime).Seconds * (baseController.Player.MaxHealth / 25);
                    baseController.Damage?.Radiation(radiationDamage);
                    lastDamagedTime = DateTime.Now;
                }
            }
        }

        public void Check()
        {
            JClass.Checker();
            Logout();
            RadiationZone();
            BeaconSync();
        }

        private PlayerBeacon _beacon;

        public void BeaconSync()
        {
            var gameSession = baseController.Player.GetGameSession();
            if (!baseController.Active || gameSession == null) return;
            if (_beacon.InEquipmentArea != baseController.Player.State.InEquipmentArea)
            {
                //todo:fix
                //Packet.Builder.EquipReadyCommand(gameSession, baseController.Player.State.InEquipmentArea);
                _beacon.InEquipmentArea = baseController.Player.State.InEquipmentArea;
            }
            if (_beacon.InDemiZone != baseController.Player.State.InDemiZone|| _beacon.InPortalArea != baseController.Player.State.InPortalArea || _beacon.InRadiationArea != baseController.Player.State.InRadiationArea || _beacon.InTradeArea != baseController.Player.State.InTradeArea)
            {
                //todo:fix
                //Packet.Builder.BeaconCommand(gameSession);
                _beacon.InDemiZone = baseController.Player.State.InDemiZone;
                _beacon.InPortalArea = baseController.Player.State.InPortalArea;
                _beacon.InRadiationArea = baseController.Player.State.InRadiationArea;
                _beacon.InTradeArea = baseController.Player.State.InTradeArea;
            }

            if (_beacon.Repairing != baseController.Repairing)
            {
                //todo:fix
                //Packet.Builder.LegacyModule(gameSession, "0|A" +
                                            //"|RS|" +
                                            //"S|" + Convert.ToInt32(baseController.Repairing), true);
                _beacon.Repairing = baseController.Repairing;
            }
        }

        /// <summary>
        /// Executes the item function depending of the selected one
        /// </summary>
        public void UseItem(string itemId)
        {
            var player = (Player)baseController.Player;

            if (player.Settings.Slotbar._items.ContainsKey(itemId))
            {
                var item = player.Settings.Slotbar._items[itemId];

                if (item.Visible && (item.Activable || item is RocketItem))
                {
                    //This is the magic function :D
                    item.Execute(player);
                }
            }
        }

        public void ChangeConfig(int targetConfigId = 0)
        {
            var gameSession = World.StorageManager.GetGameSession(baseController.Player.Id);

            if (baseController.Character.Cooldowns.Any(x => x is ConfigCooldown))
            {
                //todo:fix
                //Packet.Builder.LegacyModule(gameSession
                //, "0|A|STM|config_change_failed_time");
                return;
            }

            baseController.Character.Cooldowns.Add(new ConfigCooldown());

            targetConfigId = baseController.Player.CurrentConfig == 2 ? 1 : 2;

            baseController.Player.CurrentConfig = targetConfigId;
            baseController.Player.Updaters.Update();                    //todo:fix

            //Packet.Builder.LegacyModule(gameSession
            //    , "0|A|CC|" + baseController.Player.CurrentConfig);

            baseController.Player.UpdateExtras();

            foreach (var playerEntity in baseController.Player.Spacemap.Entities.Values.Where(x => x is Player))
            {
                var player = playerEntity as Player;
                var entitySession = player.GetGameSession();
                if (gameSession != null)
                {
                    //todo:fix
                    //Packet.Builder.DronesCommand(entitySession, baseController.Player);
                }
            }

            if (baseController.Player.Moving)
                MovementController.Move(baseController.Player, baseController.Player.Destination);
        }

        private class jClass
        {
            private DateTime JumpEndTime = new DateTime(2017, 1, 24, 0, 0, 0);

            private PlayerController baseController;

            private int TargetVirtualWorldId { get; set; }
            private Spacemap TargetMap { get; set; }
            private Vector TargetPosition { get; set; }

            public jClass(PlayerController baseController)
            {
                this.baseController = baseController;
            }

            public void Initiate(int targetVW, int targetMapId, Vector targetPos, int portalId = -1)
            {
                if (baseController.Character.EntityState == EntityStates.DEAD || baseController.StopController) return;

                TargetVirtualWorldId = targetVW;
                TargetMap = World.StorageManager.Spacemaps[targetMapId];
                TargetPosition = targetPos;

                var gameSession = World.StorageManager.GetGameSession(baseController.Player.Id);
                if (TargetMap.Level > baseController.Player.Information.Level.Id)
                {                    //todo:fix

                    //Packet.Builder.LegacyModule(gameSession, $"0|k|{TargetMap.Level}");
                    Cancel();
                    return;
                }
                if (portalId != -1)
                {
                    //todo:fix
 //                   Packet.Builder.ActivatePortalCommand(gameSession, baseController.Player.Spacemap.Objects[portalId] as Jumpgate);
                }
                JumpEndTime = DateTime.Now.AddSeconds(3);
                baseController.Jumping = true;
            }

            public void Checker()
            {
                if (baseController.Jumping) Refresh();
            }

            void Refresh()
            {
                if (baseController.Character.EntityState == EntityStates.DEAD || baseController.StopController)
                {
                    Cancel();
                    return;
                }

                if (baseController.Attack.GetActiveAttackers().Any(x => x.InRange(baseController.Character, x.AttackRange)) && baseController.Player.Spacemap.Pvp)
                {
                    Cancel();
                    return;
                }

                if (DateTime.Now > JumpEndTime)
                {
                    baseController.Miscs.ForceChangeMap(TargetMap, TargetPosition, TargetVirtualWorldId);
                    Reset();
                }
            }

            void Cancel()
            {
                Reset();
            }

            void Reset()
            {
                baseController.Jumping = false;
                JumpEndTime = DateTime.Now;
                TargetMap = null;
                TargetPosition = null;
            }
        }

        public void Jump(int targetMapId, Vector targetPos, int portalId = -1, int targetVW = 0)
        {
            JClass.Initiate(targetVW, targetMapId, targetPos, portalId);
        }

        public void ForceChangeMap(Spacemap targetMap, Vector targetPosition, int vw = 0)
        {
            if (baseController.Player.Spacemap == targetMap) return;
            //baseController.Player.Pet?.Controller.Deactivate();
            var gameSession = World.StorageManager.GetGameSession(baseController.Player.Id);
            //todo:fix
            //Packet.Builder.MapChangeCommand(gameSession);
            baseController.Destruction.Deselect(baseController.Player);
            gameSession.Relog(targetMap, targetPosition);
            gameSession.Player.VirtualWorldId = vw;
            //baseController.Player.Position = targetPosition;
            //baseController.Player.Spacemap = targetMap;
            //baseController.Player.Save();
        }

        public void ChangeDroneFormation(DroneFormation targetFormation)
        {
            //if (
            //    !baseController.CooldownStorage.Finished(
            //        objects.world.storages.playerStorages.CooldownStorage.DRONE_FORMATION_COOLDOWN)) return;

            //var gameSession = World.StorageManager.GetGameSession(baseController.Player.Id);
            //baseController.Player.Formation = targetFormation;
            //gameSession.Client.Send(DroneFormationChangeCommand.write(baseController.Player.Id, (int)targetFormation));
            //baseController.CooldownStorage.Setup(gameSession, objects.world.storages.playerStorages.CooldownStorage.DRONE_FORMATION_COOLDOWN);
            //baseController.Player.Update();
        }

        private DateTime LastReloadedTime = new DateTime(2016, 1, 1, 0, 0, 0);
        public void ReloadConfigs()
        {
            
        }
    }
}
