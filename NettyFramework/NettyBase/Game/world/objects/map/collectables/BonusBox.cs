﻿using System;
using System.Collections.Generic;
using NettyBase.Game.world.objects.characters;
using NettyBase.Game.world.objects.players.equipment;

namespace NettyBase.Game.world.objects.map.collectables
{
    class BonusBox : Collectable
    {
        public static List<Tuple<string, int>> REWARDS = new List<Tuple<string, int>>();
        public static int SPAWN_COUNT = 0;
        public static int PVP_SPAWN_COUNT = 0;

        private bool Respawning { get; }
        public BonusBox(int id, string hash, Vector pos, Spacemap map, int[] limits, bool respawning = false) : base(id, hash, Types.BONUS_BOX, pos, map, limits)
        {
            Respawning = respawning;
        }

        public override void Dispose()
        {
            base.Dispose();
            if (Respawning)
                Respawn();
        }

        protected override void Reward(Player player)
        {
            var random = new System.Random();
            var randomRewardIndex = random.Next(0, REWARDS.Count - 1);
            var rewardListIndex = REWARDS[randomRewardIndex];
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