using NettyBase.Game.world.objects.characters.cooldowns;

namespace NettyBase.Game.world.objects.players.extra.techs
{
    class ChainImpulse : Tech
    {
        public ChainImpulse(Player player) : base(player)
        {
        }

        public override void Tick()
        {
        }

        public override void execute()
        {
            if (Player.Cooldowns.Any(x => x is ChainImpulseCooldown) || Player.State.InDemiZone) return;
            Player.Controller.Damage?.ECI();
            Disable();
        }

        private void Disable()
        {
            var cld = new ChainImpulseCooldown();
            cld.Send(Player.GetGameSession());
            Player.Cooldowns.Add(cld);
        }
    }
}
