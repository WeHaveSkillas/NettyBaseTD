using System.Collections.Generic;
using NettyFramework.Utils;

namespace NettyFramework.Commands
{
    public class ClientUITooltipTextFormat
    {
        public static short STANDARD = 0;
        public static short RED = 1;

        public const short ID = 1623;

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
            foreach (var c in vard3d)
            {
                cmd.AddBytes(c.write());
            }
            cmd.writeUTF(baseKey);
            cmd.writeShort(-20953);
            cmd.AddBytes(varT1J.write());
            cmd.writeShort(textColor);
            return cmd.Message.ToArray();
        }
    }
}
