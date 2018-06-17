using System.Collections.Generic;

namespace NettyBase.Game.world.objects.players.quests
{
    class QuestRoot
    {
        public int Id { get; set; }

        public bool Active { get; set; }

        public bool Ordered { get; set; }

        public bool Mandatory { get; set; }
        
        public int MandatoryCount { get; set; }

        public List<QuestElement> Elements { get; set; }

        public QuestRoot()
        {
            Elements = new List<QuestElement>();
        }

        public void LoadPlayerData(Player player)
        {
            if (player == null) return;
            //Element[0]
            //[0, {true, 10, false}]
            //Elements[0].Condition.State.Completed = true;
            //Elements[0].Condition.State.CurrentValue = 10;
            //Elements[0].Condition.State.Active = false;
        }
    }
}
