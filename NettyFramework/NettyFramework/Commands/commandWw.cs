using NettyFramework.Utils;

namespace NettyFramework.Commands
{
    public class commandWw
    {
        public const short ID = 2274;

        public static short LO = 7;

        public static short n31 = 4;

        public static short PLAIN = 0;

        public static short x1 = 2;

        public static short A36 = 3;

        public static short B4t = 1;

        public static short LOCALIZED = 5;

        public static short Yv = 6;

        public short type;

        public commandWw(short type)
        {
            this.type = type;
        }

        public byte[] write()
        {
            var cmd = new ByteArray(ID);
            cmd.writeShort(1129);
            cmd.writeShort(type);
            return cmd.Message.ToArray();
        }

    }
}
