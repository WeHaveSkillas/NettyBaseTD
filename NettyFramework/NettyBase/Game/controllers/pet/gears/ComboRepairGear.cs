﻿using System;
using NettyBase.Game.world.objects;
using NettyBase.Game.world.objects.characters.cooldowns;
using NettyBase.Game.world.objects.pets;

namespace NettyBase.Game.controllers.pet.gears
{
    class ComboRepairGear : Gear
    {
        public int HealAmount
        {
            get
            {
                switch (Level)
                {
                    case 1:
                        return 10000;
                    case 2:
                        return 15000;
                    case 3:
                        return 25000;
                    default: return 0;
                }
            }
        }

        public ComboRepairGear(PetController controller) : base(controller, true, 3)
        {
            Type = GearType.COMBO_SHIP_REPAIR;
        }

        public override void Activate()
        {
            var owner = baseController.Pet.GetOwner();
            if (owner != null)
            {
                if (Active || owner.Cooldowns.Any(x => x is PetComboRepairCooldown) || baseController.Pet.GetOwner().LastCombatTime.AddSeconds(1) >= DateTime.Now) return;
                Active = true;

                baseController.Heal.HealingId = owner.Id;
                baseController.Heal.Amount = HealAmount;
                baseController.Heal.HealType = HealType.HEALTH;
                baseController.Heal.Healing = true;
                PulseActivationTime = DateTime.Now;
            }
        }

        public override void Check()
        {
            CheckPulse();
            Follow(baseController.Pet.GetOwner());
        }

        public override void End(bool shutdown = false)
        {
            baseController.Heal.Healing = false;
            Active = false;
            if (!shutdown)
            {
                var cld = new PetComboRepairCooldown();
                var owner = baseController.Pet.GetOwner();
                cld.Send(owner.GetGameSession());
                owner.Cooldowns.Add(cld);
            }
        }

        private DateTime PulseActivationTime = new DateTime();
        public void CheckPulse()
        {
            if (!Active || PulseActivationTime.AddSeconds(5) > DateTime.Now || baseController.Pet.GetOwner().LastCombatTime.AddSeconds(1) >= DateTime.Now) return;
            
            End();
        }
    }
}
