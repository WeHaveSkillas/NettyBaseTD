namespace NettyBase.Game.world.objects.map
{
    abstract class Zone
    {
        public int Id { get; }

        public Vector TopLeft { get; set; }
        public Vector BottomRight { get; set; }

        public Faction ZoneFaction;

        public Zone(int id, Vector topLeft, Vector botRight, Faction zoneFaction)
        {
            Id = id;
            TopLeft = topLeft;
            BottomRight = botRight;
            ZoneFaction = zoneFaction;
        }
    }
}
