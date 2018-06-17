﻿using System.Collections.Generic;
using NettyFramework.Commands;

namespace NettyBase.Game.world.objects.players.settings.slotbars
{
    class MineItem : SlotbarItem
    {
        public MineItem(string itemId, int counterValue, int maxCounter, List<ClientUITooltipTextFormat> toolTipItemBar = null, short counterType = 2, bool selected = false, bool visible = true, bool buyable = false) : base(itemId, counterValue, maxCounter, toolTipItemBar, counterType, selected, visible, buyable)
        {
        }

        public override void Execute(Player player)
        {
            int id;
            string hash;
            switch (ItemId)
            {
                case "ammunition_mine_acm-01":
                    //id = player.Spacemap.GetNextObjectId();
                    //hash = player.Spacemap.HashedObjects.Keys.ToList()[id];
                    //player.Spacemap.AddObject(new ACM01(id, hash, player.Position, player.Spacemap));
                    break;
            }
        }
    }
}
