using NettyFramework.Utils;

namespace NettyFramework.Commands
{
    public class SlotbarCategoryItemTimerModule
    {
        public const short ID = 23030;

        public TimerState timerState;
        public string var14v = "";
        public double time = 0;
        public bool activatable = false;
        public double maxTime = 0;

        public SlotbarCategoryItemTimerModule(string var14v, TimerState timerState, double time, double maxTime, bool activatable)
        {
            this.var14v = var14v;
            this.timerState = timerState;
            this.time = time;
            this.maxTime = maxTime;
            this.activatable = activatable;
        }

        public byte[] write()
        {
            var cmd = new ByteArray(ID);
            cmd.writeDouble(this.maxTime);
            cmd.AddBytes(timerState.write());
            cmd.writeBoolean(this.activatable);
            cmd.writeDouble(this.time);
            cmd.writeUTF(var14v);
            return cmd.Message.ToArray();
        }

        public byte[] write2()
        {
            var cmd = new ByteArray(ID);
            cmd.writeDouble(this.maxTime);
            cmd.AddBytes(timerState.write());
            cmd.writeBoolean(this.activatable);
            cmd.writeDouble(this.time);
            cmd.writeUTF(var14v);
            return cmd.ToByteArray();
        }
    }
}
