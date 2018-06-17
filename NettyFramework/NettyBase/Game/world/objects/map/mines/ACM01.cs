namespace NettyBase.Game.world.objects.map.mines
{
    class ACM01 : Mine
    {
        public override int MineType => 1;

        public ACM01(int id, string hash, Vector pos, Spacemap map) : base(id, hash, pos, map)
        {
        }
    }
}
