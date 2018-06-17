using System.Collections.Generic;
using NettyFramework.Utils;

namespace NettyFramework.Commands
{
    public class ClientUITooltipTextFormat
    {
        public static short STANDARD = 0;
        public static short RED = 1;

        public const short ID = 18148;

        public List<commandF5> vard3d;

        public string baseKey = "";

        public commandWw varT1J;

        public short textColor = 0;

        public ClientUITooltipTextFormat(short textColor, string baseKey, commandWw varT1J, List<commandF5> vard3d)
        {
            this.textColor = textColor;
            this.baseKey = baseKey;
            this.varT1J = varT1J;
            this.vard3d = vard3d;
        }

        public byte[] write()
        {
            var cmd = new ByteArray(ID);
            cmd.writeInt(vard3d.Count);
            foreach(var _loc2_ in this.vard3d)
            {
                cmd.AddBytes(_loc2_.write());
            }
            cmd.writeShort(textColor);
            cmd.writeUTF(this.baseKey);
            cmd.AddBytes(varT1J.write());
            return cmd.Message.ToArray();
        }
    }
}
