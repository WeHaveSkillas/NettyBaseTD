using System;
using System.Linq;
using NettyBase.Game.controllers.implementable;
using NettyBase.Game.world.objects;
using NettyBase.Game.world.objects.map.objects;
using NettyBase.Game.world.objects.map.zones;
using NettyBase.Logger.types;

namespace NettyBase.Game.controllers.player
{
    class Range : IChecker
    {
        private PlayerController baseController;

        public Range(PlayerController controller)
        {
            baseController = controller;
        }

        public void Check()
        {
            LookupRangeZones();
            LookupRangeObjects();
        }

        private DateTime LastTimeCheckedZones = new DateTime();
        public void LookupRangeZones()
        {
            if (LastTimeCheckedZones.AddMilliseconds(250) > DateTime.Now) return;

            try
            {
                if (baseController.Player.Range.Zones.Values.Count(x => x is DemiZone && (x.ZoneFaction == baseController.Player.FactionId || x.ZoneFaction == Faction.NONE)) > 0)
                {
                    if (!baseController.Player.State.InDemiZone && !baseController.Attack.Attacking)
                    {
                        baseController.Player.State.InDemiZone = true;
                    }
                }
                else
                {
                    if (baseController.Player.State.InDemiZone)
                    {
                        baseController.Player.State.InDemiZone = false;
                    }
                }
            }
            catch (Exception e)
            {
                new ExceptionLog("player_range", "Range Zones", e);
            }
            LastTimeCheckedZones = DateTime.Now;
        }

        private DateTime LastTimeCheckedObjects = new DateTime();
        public void LookupRangeObjects()
        {
            if (LastTimeCheckedObjects.AddMilliseconds(3000) > DateTime.Now) return;

            //TODO
            try
            {
                if (baseController.Player.Range.Objects.Values.Any(x => x is Station))
                {
                    var session = baseController.Player.GetGameSession();
                    if (session != null)
                    {
                        baseController.Player.State.InEquipmentArea = true;
                        //Packet.Builder.EquipReadyCommand(session, true);
                        //World.DatabaseManager.Refresh(baseController.Player);
                        //baseController.Player.Equipment.Hangars =
                        //    World.DatabaseManager.LoadHangars(baseController.Player);
                        //baseController.Player.Hangar.Drones = World.DatabaseManager.LoadDrones(baseController.Player);
                        //baseController.Player.Hangar.Configurations =
                        //    World.DatabaseManager.LoadConfig(baseController.Player);
                    }
                }
                else
                {
                    //var session = baseController.Player.GetGameSession();
                    //if (session != null)
                    //    Packet.Builder.EquipReadyCommand(session, false);
                    baseController.Player.State.InEquipmentArea = false;
                }
            }
            catch (Exception e)
            {
                new ExceptionLog("player_range_object", "Range Objects", e);
            }
            LastTimeCheckedObjects = DateTime.Now;
        }
    }
}
