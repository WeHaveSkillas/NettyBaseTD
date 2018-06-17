using System;

namespace NettyBase.Game.world.objects.characters.cooldowns
{
    class EnergyLeechCooldown : Cooldown
    {
        public EnergyLeechCooldown() : base(DateTime.Now, DateTime.Now.AddSeconds(900))
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
            //Packet.Builder.LegacyModule(gameSession, "0|A|CLD|ELA|900");
            //TODO: do for new client too
        }
    }
}
