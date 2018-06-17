using NettyBase.Game.controllers.player;

namespace NettyBase.Game.world.objects.players.equipment.extras
{
    class AutoRocketLauncher : Extra
    {
        public AutoRocketLauncher(Player player, int itemId, string lootId, int amount) : base(player, itemId, lootId, amount)
        {
        }

        public override void execute()
        {
            base.execute();
            Player.Controller.CPUs.Activate(CPU.Types.AUTO_ROCKLAUNCHER);
        }
    }
}
