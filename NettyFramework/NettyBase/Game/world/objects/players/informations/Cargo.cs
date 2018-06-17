namespace NettyBase.Game.world.objects.players.informations
{
    class Cargo : OreBase
    {
        private Player Player { get; }
        public Cargo(Player player, int prometium, int endurium,
                        int terbium, int prometid, int duranium, int xenomit, int promerium, int seprom) : base(promerium, endurium, terbium, prometid, duranium, xenomit, promerium, seprom)
        {
            Player = player;
        }

        private void Add()
        {

        }
    }
}
