using System;
using NettyBase.Game.world.objects.characters.cooldowns;

namespace NettyBase.Game.world.objects.players.extra.abilities
{
    class SentinelFortress : Ability
    {
        public SentinelFortress(Player player) : base(player, Abilities.SHIP_ABILITY_SENTINEL_FORTRESS)
        {
        }

        public override void Tick()
        {
            Update();
        }

        public override void execute()
        {
            if (!Enabled) return;
            Active = true;
            TargetIds.Add(Player.Id);
            TimeFinish = DateTime.Now.AddMinutes(2);
            Start();
        }

        public override void ThreadUpdate()
        {
            if (TimeFinish < DateTime.Now)
            {
                End();
                Active = false;
                Cooldown = new SentinelFortressCooldown(Player);
            }
        }
    }
}
