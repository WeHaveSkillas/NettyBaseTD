using NettyBase.Game;
using NettyBase.Game.world.objects;

namespace NettyBase.Main.objects
{
    class ClanMember
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Player Player => World.StorageManager.GetGameSession(Id)?.Player;
    }
}
