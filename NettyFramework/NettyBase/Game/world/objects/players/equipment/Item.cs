namespace NettyBase.Game.world.objects.players.equipment
{
    class Item
    {
        public int Id { get; set; }

        public string LootId { get; set; }

        public int Amount { get; set; }

        public Item(int id, string lootId, int amount)
        {
            Id = id;
            LootId = lootId;
            Amount = amount;
        }
    }
}
