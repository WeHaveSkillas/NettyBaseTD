﻿using System;
using System.Linq;
using NettyBase.Game.world.objects;

namespace NettyBase.Game.controllers.npc
{
    class Daughter : INpc
    {
        private NpcController Controller { get; set; }

        public Daughter(NpcController controller)
        {
            Controller = controller;
            LastActiveTime = DateTime.Now;
        }

        public void Tick()
        {
            PositionChecker();
            if (Controller.Npc.SelectedCharacter == null)
                Inactive();
            else Active();
        }

        private DateTime LastActiveTime = new DateTime();
        public void Active()
        {
            if (!Controller.Npc.SelectedCharacter.InRange(Controller.Npc))
            {
                Controller.Npc.Selected = null;
                return;
            }
            Controller.Attack.Attacking = true;
            LastActiveTime = DateTime.Now;
        }

        public void Inactive()
        {
            if (LastActiveTime.AddSeconds(30) <= DateTime.Now)
            {
                Exit();
            }

            var attacker = Controller.Npc.MotherShip.Controller.Attack.GetActiveAttackers().OrderBy(x => x.Damage).FirstOrDefault();
            if (attacker == null)
            {
                var rangePlayers = Controller.Npc.Range.Entities.Where(x => x.Value is Player);
                attacker = rangePlayers.FirstOrDefault().Value;
            }

            Controller.Npc.Selected = attacker;
        }

        public void PositionChecker()
        {
            if (Controller.Npc.MovementStartTime.AddSeconds(1) <= DateTime.Now)
                MovementController.Move(Controller.Npc, Vector.GetPosOnCircle(Controller.Npc.MotherShip.Position, 400));
        }

        public void Paused()
        {
            throw new NotImplementedException();
        }

        public void Exit()
        {
            // TODO Remove from map
        }

    }
}
