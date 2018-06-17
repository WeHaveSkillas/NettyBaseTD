namespace NettyBase.Game.world.objects.players.equipment.extras
{
    class Turbo : Extra
    {
        public Turbo(Player player, int itemId, string lootId, int amount) : base(player, itemId, lootId, amount)
        {
            Active = true;
        }

        public override void execute()
        {
            base.execute();
        }
    }
}
