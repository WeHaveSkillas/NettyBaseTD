using System;
using NettyBase.Game.world.objects.players.extra.abilities;

namespace NettyBase.Game.world.objects.characters.cooldowns
{
    class AegisHealBeamCooldown : Cooldown
    {
        public AegisHealBeamCooldown(AegisHealBeam ability) : base(DateTime.Now, DateTime.Now.AddSeconds(90))
        {
            Send(ability.Player.GetGameSession());
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
            //Packet.Builder.LegacyModule(gameSession, "0|A|CLD|HPA|" + TotalTime);
        }
    }
}
