using System;
using NettyBase.Game.world.objects;
using NettyBase.Game.world.objects.characters.cooldowns;
using NettyBase.Networking.game_server;

namespace NettyBase.Game.controllers.implementable
{
    class Effects : IAbstractCharacter
    {
        public bool SlowedDown { get; set; }

        public Effects(AbstractCharacterController controller) : base(controller)
        {
        }

        public override void Tick()
        {
            //throw new NotImplementedException();
        }

        public override void Stop()
        {
            throw new NotImplementedException();
        }

        public void Slowdown(Character targetCharacter)
        {
            //TODO
            //Todo: fix
            //GameClient.SendToSpacemap(targetCharacter.Spacemap, netty.commands.new_client.LegacyModule.write("0|n|fx|start|GRAPHIC_FX_SABOTEUR_DEBUFF|" + targetCharacter.Id));
        }

        public void SetInvincible(int time, bool showEffect = false)
        {
            if (Character.Cooldowns.Any(x => x is InvincibilityCooldown)) return;

            var cooldown = new InvincibilityCooldown(showEffect, DateTime.Now.AddMilliseconds(time));
            Character.Cooldowns.Add(cooldown);
            cooldown.OnStart(Character);
        }

        public void NotTargetable(int time)
        {
            if (Character.Cooldowns.Any(x => x is NonTargetableCooldown)) return;

            var cooldown = new NonTargetableCooldown(DateTime.Now.AddMilliseconds(time));
            Character.Cooldowns.Add(cooldown);
            cooldown.OnStart(Character);
        }

        public void UpdatePlayerVisibility()
        {
            //TOdo : fix
            //GameClient.SendPacketSelected(Controller.Character,
            //    netty.commands.new_client.LegacyModule.write("0|n|INV|" + Controller.Character.Id + "|" +
            //                                                 Convert.ToInt32(Controller.Character.Invisible)));
            //GameClient.SendRangePacket(Controller.Character,
            //    netty.commands.new_client.LegacyModule.write("0|n|INV|" + Controller.Character.Id + "|" +
            //                                                 Convert.ToInt32(Controller.Character.Invisible)), true);
        }
    }
}
