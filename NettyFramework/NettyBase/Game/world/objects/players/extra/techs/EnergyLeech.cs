using System;
using NettyBase.Game.world.objects.characters.cooldowns;

namespace NettyBase.Game.world.objects.players.extra.techs
{
    class EnergyLeech : Tech
    {
        EnergyLeechCooldown cld = new EnergyLeechCooldown();

        public EnergyLeech(Player player) : base(player)
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
            if (Player.Cooldowns.Any(x => x is EnergyLeechCooldown)) return;
            Active = true;
            Player.Storage.EnergyLeechActivated = true;
            //todo: fix
            //Packet.Builder.TechStatusCommand(Player.GetGameSession());
            //GameClient.SendToPlayerView(Player, netty.commands.new_client.LegacyModule.write("0|TX|A|S|ELA|" + Player.Id), true);
            TimeFinish = DateTime.Now.AddSeconds(900);
            Player.Cooldowns.Add(cld);
        }

        public void ExecuteHeal(int damage)
        {
            if (TimeFinish > DateTime.Now) {
                var lastDamage = (damage / 100) * 10;
                Player.Controller.Heal.Execute(lastDamage);
            }
        }

        private void Disable()
        {
            Active = false;
            Player.Storage.EnergyLeechActivated = false;
            //todo: fix
            //Packet.Builder.TechStatusCommand(Player.GetGameSession());
            //GameClient.SendToPlayerView(Player, netty.commands.new_client.LegacyModule.write("0|TX|D|S|ELA|" + Player.Id), true);
            cld.Send(Player.GetGameSession());
        }
    }
}
