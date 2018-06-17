using System;
using NettyFramework.Commands;

namespace NettyBase.Game.world.objects.characters.cooldowns
{
    class RSBCooldown : Cooldown
    {
        internal RSBCooldown() : base(DateTime.Now, DateTime.Now.AddSeconds(3)) { }

        public override void OnStart(Character character)
        {
            base.OnStart(character);
        }

        public override void OnFinish(Character character)
        {
        }

        public override void Send(GameSession gameSession)
        {
            var player = gameSession.Player;

            var item = player.Settings.CurrentAmmo;
                gameSession.Client.Send(SetCooldown(item.LootId, TimerState.COOLDOWN, 3000, 3000, true));
         }
    }
}