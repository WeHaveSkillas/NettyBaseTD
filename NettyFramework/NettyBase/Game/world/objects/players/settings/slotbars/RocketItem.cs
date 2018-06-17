using System.Collections.Generic;
using NettyFramework.Commands;

namespace NettyBase.Game.world.objects.players.settings.slotbars
{
    class RocketItem : SlotbarItem
    {
        public RocketItem(string itemId, int counterValue, int maxCounter,
            List<ClientUITooltipTextFormat> tooltips = null, short counterType = 2,
            bool selected = false, bool visible = true, bool buyable = false) : base(itemId, counterValue, maxCounter,
            tooltips, counterType, selected, visible, buyable)
        {
        }

        public override void Execute(Player player)
        {
            var gameSession = World.StorageManager.GameSessions[player.Id];

            foreach (var item in player.Settings.Slotbar._items)
            {
                var value = item.Value;

                if (value is RocketItem)
                {
                    value.Selected = false;
                    gameSession.Client.Send(value.ChangeStatus());
                }
            }

            player.Settings.CurrentRocket = player.Information.Ammunitions[ItemId];

            Selected = true;

            gameSession.Client.Send(ChangeStatus());
        }
    }
}
