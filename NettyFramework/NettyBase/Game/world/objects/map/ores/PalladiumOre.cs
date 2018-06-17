using NettyBase.Game.world.objects.characters;
using NettyBase.Game.world.objects.map.collectables;
using NettyBase.Game.world.objects.players.equipment;

namespace NettyBase.Game.world.objects.map.ores
{
    class PalladiumOre : Ore
    {
        public PalladiumOre(int id, string hash, OreTypes type, Vector pos, Spacemap map, int[] limits) : base(id, hash, type, pos, map, limits)
        {
        }

        public override void Collect(Character character)
        {
            base.Collect(character);
            Reward(character as Player);
        }

        public void Reward(Player player)
        {
            if (player == null) return;
            var random = new System.Random();
            var randomRewardIndex = random.Next(0, BonusBox.REWARDS.Count - 1);
            var rewardListIndex = BonusBox.REWARDS[randomRewardIndex];
            var lootId = rewardListIndex.Item1;
            var amount = rewardListIndex.Item2;
            RewardType type;
            Reward reward;
            if (RewardType.TryParse(lootId, true, out type))
            {
                reward = new Reward(type, amount);
            }
            else
            {
                if (lootId.Contains("ammunition"))
                    type = RewardType.AMMO;
                reward = new Reward(type, new Item(-1, lootId, amount), amount);
            }
            reward.ParseRewards(player);
        }
    }
}
