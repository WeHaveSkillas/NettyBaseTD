﻿using System.Collections.Generic;

namespace NettyBase.Game.world.objects.players.quests
{
    class QuestElement
    {
        public QuestRoot Case { get; set; }
        public QuestCondition Condition { get; set; }

        //public static List<netty.commands.old_client.QuestElementModule> ParseElementsOld(List<QuestElement> elements)
        //{
        //    var questElements = new List<netty.commands.old_client.QuestElementModule>();
        //    foreach (var questElement in elements)
        //    {
        //        questElements.Add(new netty.commands.old_client.QuestElementModule(new netty.commands.old_client.QuestCaseModule(questElement.Case.Id, questElement.Case.Active, questElement.Case.Mandatory, questElement.Case.Ordered, questElement.Case.MandatoryCount, ParseElementsOld(questElement.Case.Elements)),
        //            new QuestConditionModule(questElement.Condition.Id, questElement.Condition.Matches, (short) questElement.Condition.Type, (short) questElement.Condition.Type, questElement.Condition.TargetValue, questElement.Condition.Mandatory, new QuestConditionStateModule(questElement.Condition.State.CurrentValue, questElement.Condition.State.Active, questElement.Condition.State.Completed), 
        //                new List<QuestConditionModule>())));
        //    }
        //    return questElements;
        //} todo: fix for new client

        //public static List<netty.commands.old_client.LootModule> ParseRewardsOld(Reward rewards)
        //{
        //    foreach (var reward in rewards.Rewards)
        //    {
                
        //    }
        //}

        //TODO: Add new
    }
}
