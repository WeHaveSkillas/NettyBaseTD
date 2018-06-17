using NettyBase.Main;

namespace NettyBase.Game.world.objects.map.objects.assets
{
    class QuestGiver : Asset, IClickable
    {
        public int QuestGiverId;

        public QuestGiver(int id, int questGiverId, Faction faction, Vector pos, Spacemap map) : base(id, "Nyx", AssetTypes.QUESTGIVER, faction, Global.StorageManager.Clans[0], 1, 0, pos, map, false, false, false)
        {
            QuestGiverId = questGiverId;
        }

        public void click(Character character)
        {
            if (character is Player player)
            {
                //Packet.Builder.QuestGiversAvailableCommand(player.GetGameSession(), this);
            }
        }
    }
}
