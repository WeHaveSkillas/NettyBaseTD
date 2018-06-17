using System;
using NettyFramework.Commands;

namespace NettyBase.Game.world.objects.characters.cooldowns
{
    class EMPCooldown : Cooldown
    {
        internal EMPCooldown() : base(DateTime.Now, DateTime.Now.AddSeconds(30))
        {
        }

        public override void OnStart(Character character)
        {
            base.OnStart(character);
        }

        public override void OnFinish(Character character)
        {

        }

        public override void Send(GameSession gameSession)
        {

            gameSession.Client.Send(SetCooldown("ammunition_specialammo_emp-01", TimerState.COOLDOWN, 30000, 30000,
                true));

        }
    }
}