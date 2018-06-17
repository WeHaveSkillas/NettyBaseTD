namespace NettyBase.Game.world.objects.players
{
    class PlayerBaseClass
    {
        /// <summary>
        /// Base class for all the /players/ classes
        /// </summary>

        internal Player Player { get; }

        public PlayerBaseClass(Player player)
        {
            Player = player;
        }
    }
}
