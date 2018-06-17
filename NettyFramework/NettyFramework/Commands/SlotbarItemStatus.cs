﻿using NettyFramework.Utils;

namespace NettyFramework.Commands
{
    public class SlotbarItemStatus
    {
        public const short ID = 15995;

        public static short ORANGE = 6;
        public static short GREEN = 2;
        public static short DEFAULT = 0;
        public static short YELLOW = 3;
        public static short BLUE = 4;
        public static short LIGHT_BLUE = 5;
        public static short RED = 1;

        public string iconLootId = "";
        public bool selected = false;
        public bool activatable = false;
        public bool buyable = false;
        public string clickedId = "";
        public double maxCounterValue = 0;
        public bool blocked = false;
        public ClientUITooltip toolTipSlotBar;
        public bool visible = false;
        public short counterStyle = 0;
        public bool available = false;
        public ClientUITooltip toolTipItemBar;
        public double counterValue = 0;

        public SlotbarItemStatus(string iconLootId, bool selected, bool activatable, bool buyable, string clickedId, double maxCounterValue, bool blocked, ClientUITooltip toolTipSlotBar, bool visible, short counterStyle, bool available, ClientUITooltip toolTipItemBar, double counterValue)
        {
            this.iconLootId = iconLootId;
            this.selected = selected;
            this.activatable = activatable;
            this.buyable = buyable;
            this.clickedId = clickedId;
            this.maxCounterValue = maxCounterValue;
            this.blocked = blocked;
            this.toolTipSlotBar = toolTipSlotBar;
            this.visible = visible;
            this.counterStyle = counterStyle;
            this.available = available;
            this.toolTipItemBar = toolTipItemBar;
            this.counterValue = counterValue;
        }

        public byte[] write()
        {
            var cmd = new ByteArray(ID);
            cmd.writeUTF(iconLootId);
            cmd.writeBoolean(selected);
            cmd.writeBoolean(activatable);
            cmd.writeBoolean(buyable);
            cmd.writeUTF(clickedId);
            cmd.writeDouble(maxCounterValue);
            cmd.writeBoolean(blocked);
            cmd.AddBytes(toolTipSlotBar.write());
            cmd.writeBoolean(visible);
            cmd.writeShort(counterStyle);
            cmd.writeBoolean(available);
            cmd.AddBytes(toolTipItemBar.write());
            cmd.writeShort(9774);
            cmd.writeShort(-14937);
            cmd.writeDouble(counterValue);
            return cmd.Message.ToArray();
        }

        public byte[] write2()
        {
            var cmd = new ByteArray(ID);
            cmd.writeUTF(iconLootId);
            cmd.writeBoolean(selected);
            cmd.writeBoolean(activatable);
            cmd.writeBoolean(buyable);
            cmd.writeUTF(clickedId);
            cmd.writeDouble(maxCounterValue);
            cmd.writeBoolean(blocked);
            cmd.AddBytes(toolTipSlotBar.write());
            cmd.writeBoolean(visible);
            cmd.writeShort(counterStyle);
            cmd.writeBoolean(available);
            cmd.AddBytes(toolTipItemBar.write());
            cmd.writeShort(9774);
            cmd.writeShort(-14937);
            cmd.writeDouble(counterValue);
            return cmd.ToByteArray();
        }

    }
}
