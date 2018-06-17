using NettyFramework.Utils;

namespace NettyFramework.Commands
{
    public class TimerState
    {
        public const short ID = 23246;

        public static short COOLDOWN = 2;
        public static short READY = 0;
        public static short ACTIVE = 1;

        public short value;

        public TimerState(short value)
        {
            this.value = value;
        }

        public byte[] write()
        {
            var cmd = new ByteArray(ID);
            cmd.writeShort(26576);
            cmd.writeShort(value);
            cmd.writeShort(-5564);
            return cmd.Message.ToArray();
        }

    }
}
