using System;
using NettyBase.Game.world.objects.characters.cooldowns;
using NettyBase.Networking.game_server;

namespace NettyBase.Game.world.objects.players.extra.techs
{
    class BattleRepairRobot : Tech
    {
        BattleRepairRobotCooldown cld = new BattleRepairRobotCooldown();

        public BattleRepairRobot(Player player) : base(player)
        {
        }

        public override void Tick()
        {
            if (Active)
            {
                if (TimeFinish < DateTime.Now)
                    Disable();
            }
        }

        public override void execute()
        {
            if (Player.Cooldowns.Any(x => x is BattleRepairRobotCooldown)) return;
            Active = true;
            Player.Storage.BattleRepairRobotActivated = true;
            //todo: fix
            //Packet.Builder.TechStatusCommand(Player.GetGameSession());
            //GameClient.SendToPlayerView(Player, netty.commands.new_client.LegacyModule.write("0|TX|A|S|BRB|" + Player.Id), true);
            TimeFinish = DateTime.Now.AddSeconds(10);
            Player.Cooldowns.Add(cld);
            Start();
        }

        public override void ThreadUpdate() => ExecuteHeal();

        private void ExecuteHeal()
        {
            Player.Controller.Heal.Execute(10000);
        }

        private void Disable()
        {
            Active = false;
            Player.Storage.BattleRepairRobotActivated = false;
            //Packet.Builder.TechStatusCommand(Player.GetGameSession());
            //GameClient.SendToPlayerView(Player, netty.commands.new_client.LegacyModule.write("0|TX|D|S|BRB|" + Player.Id), true);
            //todo: fix
            cld.Send(Player.GetGameSession());
        }
    }
}
