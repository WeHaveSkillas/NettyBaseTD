using System;

namespace NettyBase.Game.world.objects.characters.cooldowns
{
    class ChainImpulseCooldown : Cooldown
    {
        public ChainImpulseCooldown() : base(DateTime.Now, DateTime.Now.AddSeconds(60))
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
            //Packet.Builder.LegacyModule(gameSession, "0|A|CLD|ECI|" + TotalTime);
            //TODO: do for new client too
        }
    }
}
