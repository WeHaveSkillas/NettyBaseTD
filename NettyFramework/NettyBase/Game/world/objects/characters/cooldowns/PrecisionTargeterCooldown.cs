using System;

namespace NettyBase.Game.world.objects.characters.cooldowns
{
    class PrecisionTargeterCooldown : Cooldown
    {
        public PrecisionTargeterCooldown() : base(DateTime.Now, DateTime.Now.AddSeconds(300))
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
            //Packet.Builder.LegacyModule(gameSession, "0|A|CLD|RPM|300");
            //TODO: do for new client too
        }
    }
}
