﻿using System;
using NettyBase.Game.world.objects.characters.cooldowns;

namespace NettyBase.Game.world.objects.players.extra.techs
{
    class RocketPrecission : Tech
    {
        PrecisionTargeterCooldown cld = new PrecisionTargeterCooldown();

        public RocketPrecission(Player player) : base(player)
        {
        }

        public override void Tick()
        {
            if (Active)
            {
                if (TimeFinish < DateTime.Now)
                    Disable();
            }
        }

        public override void execute()
        {
            if (Player.Cooldowns.Any(x => x is PrecisionTargeterCooldown)) return;
            Active = true;
            Player.Storage.PrecisionTargeterActivated = true;
            //Packet.Builder.TechStatusCommand(Player.GetGameSession());
            TimeFinish = DateTime.Now.AddSeconds(900);
            Player.Cooldowns.Add(cld);
        }

        private void Disable()
        {
            Active = false;
            Player.Storage.PrecisionTargeterActivated = false;
            //Packet.Builder.TechStatusCommand(Player.GetGameSession());
            cld.Send(Player.GetGameSession());
        }
    }
}
