﻿using System;
using NettyBase.Game.controllers.implementable;
using NettyBase.Game.controllers.pet;
using NettyBase.Game.controllers.pet.gears;
using NettyBase.Game.world.objects;
using NettyBase.Main;

namespace NettyBase.Game.controllers
{
    class PetController : AbstractCharacterController
    {
        public Pet Pet { get; }
        private Gear Gear { get; set; }

        internal event EventHandler Shutdown;

        public PetController(Character character) : base(character)
        {
            Pet = Character as Pet;
        }

        private void Start()
        {
            LoadGears();
            Initiate();
            Global.TickManager.Add(Pet);
            MovementController.Move(Pet, Vector.GetPosOnCircle(Pet.GetOwner().Position, 250));
        }

        private void LoadGears()
        {
            Pet.Gears.Add(new PassiveGear(this));
            Pet.Gears.Add(new GuardGear(this));
            //Pet.Gears.Add(new AutoLootGear(this));
            Pet.Gears.Add(new ComboRepairGear(this));
            //Pet.Gears.Add(new AutoResourceCollectionGear(this));
            Gear = Pet.Gears[0];
            var owner = Pet.GetOwner();
            var gameSession = World.StorageManager.GetGameSession(owner.Id);
            //TODO: Fix
            foreach (var gear in Pet.Gears)
            {
                //Packet.Builder.PetGearAddCommand(gameSession, gear);
            }
            //Packet.Builder.PetGearSelectCommand(gameSession, Gear);
        }

        public void Exit()
        {
            Global.TickManager.Remove(Pet);
            StopAll();
            Shutdown?.Invoke(this, EventArgs.Empty);
            Checkers.Stop();
            Pet.Gears.Clear();
            Gear = null;
        }

        public new void Tick()
        {
            Gear.Check();
        }

        public void Activate()
        {
            Pet.Position = Pet.GetOwner().Position;
            Pet.Spacemap = Pet.GetOwner().Spacemap;

            if (!Pet.Spacemap.Entities.ContainsKey(Pet.Id))
                Pet.Spacemap.AddEntity(Pet);

            var session = World.StorageManager.GetGameSession(Pet.GetOwner().Id);
            //TODO: Fix
            //Packet.Builder.PetHeroActivationCommand(session, Pet);
            //Packet.Builder.PetStatusCommand(session, Pet);
            Start();
        }

        public void Deactivate()
        {
            Exit();
            Destruction.Remove();

            var ownerSession = World.StorageManager.GetGameSession(Pet.GetOwner().Id);
            if (ownerSession != null)
            {
                //TODO: Fix
                //Packet.Builder.PetDeactivationCommand(ownerSession, Pet);
            }
        }

        public void Destroy()
        {
            Exit();
            Destruction.Remove();

            var ownerSession = World.StorageManager.GetGameSession(Pet.GetOwner().Id);
            if (ownerSession != null)
            {
                //TODO: Fix
                //Packet.Builder.PetIsDestroyedCommand(ownerSession);
                //Packet.Builder.PetUIRepairButtonCommand(ownerSession, true, 0);
            }
        }

        public void Repair()
        {
            Pet.CurrentHealth = 1000;
            Pet.EntityState = EntityStates.ALIVE;
            var ownerSession = World.StorageManager.GetGameSession(Pet.GetOwner().Id);
            if (ownerSession != null)
            {
                //TODO: Fix
                //Packet.Builder.PetRepairCompleteCommand(ownerSession);
            }
        }

        public void SwitchGear(short gearType, int optParam)
        {
            if (Gear.Active)
                Gear.End();
            var gearIndex = Pet.Gears.FindIndex(x => (short) x.Type == gearType);
            Gear = Pet.Gears[gearIndex];
            Gear.Activate();
        }
    }
}
