using NettyFramework.Utils;

namespace NettyFramework.Commands
{
    public class SlotbarCategoryItemModule
    {
        public const short ID = 19838;

        public static short c3O = 0;
        public static short Rb = 1;
        public static short TIMER = 3;
        public static short SELECTION = 2;
        public static short NONE = 0;
        public static short NUMBER = 1;
        public static short DOTS = 3;
        public static short BAR = 2;

        public int Id = 0;
        public SlotbarItemStatus status;
        public SlotbarCategoryItemTimerModule timer;
        public CooldownTypeModule varB3M;
        public short counterType = 0;
        public short actionStyle = 0;
        public bool showTooltipCooldownTimer;

        public SlotbarCategoryItemModule(int Id, SlotbarItemStatus status, SlotbarCategoryItemTimerModule timer, CooldownTypeModule varB3M, short counterType, short actionStyle, bool showTooltipCooldownTimer)
        {
            this.Id = Id;
            this.status = status;
            this.timer = timer;
            this.varB3M = varB3M;
            this.counterType = counterType;
            this.actionStyle = actionStyle;
            this.showTooltipCooldownTimer = showTooltipCooldownTimer;
        }

        public byte[] write()
        {
            var cmd = new ByteArray(ID);
            cmd.AddBytes(timer.write());
            cmd.writeShort(counterType);
            cmd.writeShort(-3158);
            cmd.AddBytes(varB3M.write());
            cmd.writeBoolean(showTooltipCooldownTimer);
            cmd.AddBytes(status.write());
            cmd.writeInt(Id >> 5 | Id << 27);
            cmd.writeShort(-10266);
            cmd.writeShort(actionStyle);
            return cmd.Message.ToArray();
        }
    }
}
