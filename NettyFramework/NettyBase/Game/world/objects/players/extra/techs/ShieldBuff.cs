using NettyBase.Game.world.objects.characters.cooldowns;
using NettyBase.Networking.game_server;

namespace NettyBase.Game.world.objects.players.extra.techs
{
    class ShieldBuff : Tech
    {
        public ShieldBuff(Player player) : base(player)
        {
        }

        public override void Tick()
        {
        }

        public override void execute()
        {
            if (Player.Cooldowns.Any(x => x is ShieldBuffCooldown)) return;
            //todo: fix
            //GameClient.SendToPlayerView(Player, netty.commands.new_client.LegacyModule.write("0|TX|A|S|SBU|" + Player.Id), true);
            ExecuteShield();
            Disable();
        }

        private void ExecuteShield()
        {
            Player.Controller.Heal.Execute(75000, 0, HealType.SHIELD);
        }

        private void Disable()
        {
            var cld = new ShieldBuffCooldown();
            cld.Send(Player.GetGameSession());
            Player.Cooldowns.Add(cld);
        }
    }
}
