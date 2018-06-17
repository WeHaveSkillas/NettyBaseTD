namespace NettyBase.Game.world.objects.map.objects
{
    class Billboard : Object
    {
        public short Advertiser;

        public Billboard(int id, Vector pos, Spacemap map, short advertiser, int range = 1000) : base(id, pos, map, range)
        {
            Advertiser = advertiser;
        }

        public override void execute(Character character)
        {
            //TODO: Show advertisement
        }
    }
}
