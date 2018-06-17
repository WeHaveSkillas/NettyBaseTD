using System.Collections.Generic;

namespace NettyBase.Game.world.objects.players.quests
{
    class QuestCondition
    {
        public int Id { get; set; }

        public QuestConditions Type { get; set; }

        public List<int> Matches { get; set; }

        public bool Mandatory { get; set; }

        public int TargetValue { get; set; }

        public QuestState State { get; set; }

        public List<QuestCondition> SubConditions { get; set; }
    }
}
