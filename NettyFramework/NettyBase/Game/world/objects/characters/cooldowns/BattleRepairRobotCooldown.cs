using System;

namespace NettyBase.Game.world.objects.characters.cooldowns
{
    class BattleRepairRobotCooldown : Cooldown
    {
        public BattleRepairRobotCooldown() : base(DateTime.Now, DateTime.Now.AddSeconds(120))
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
            //Packet.Builder.LegacyModule(gameSession, "0|A|CLD|BRB|" + TotalTime);
            //TODO: do for new client too
        }
    }
}
