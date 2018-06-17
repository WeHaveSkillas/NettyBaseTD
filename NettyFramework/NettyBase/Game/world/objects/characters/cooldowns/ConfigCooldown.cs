using System;

namespace NettyBase.Game.world.objects.characters.cooldowns
{
    class ConfigCooldown : Cooldown
    {
        public ConfigCooldown() : base(DateTime.Now, DateTime.Now.AddSeconds(3))
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
        }
    }
}
