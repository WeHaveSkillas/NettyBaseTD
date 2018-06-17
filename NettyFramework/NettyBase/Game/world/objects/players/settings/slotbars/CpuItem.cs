using System.Collections.Generic;
using NettyFramework.Commands;

namespace NettyBase.Game.world.objects.players.settings.slotbars
{
    class CpuItem : SlotbarItem
    {
        public CpuItem(string itemId, int counterValue, int maxCounter, List<ClientUITooltipTextFormat> toolTipItemBar = null, short counterType = 0, bool selected = false, bool visible = true, bool buyable = false) : base(itemId, counterValue, maxCounter, toolTipItemBar, counterType, selected, visible, buyable)
        {
        }

        public override void Execute(Player player)
        {
            player.Extras[ItemId].execute();
        }
    }
}