using System;

namespace NettyBase.Game.world.objects.characters.cooldowns
{
    class DecelerationCooldown : Cooldown
    {
        public DecelerationCooldown() : base(DateTime.Now, DateTime.Now.AddSeconds(30))
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
            //todo: fix
            //Packet.Builder.LegacyModule(gameSession, "0|A|CLD|DCR|30");
        }
    }
}
