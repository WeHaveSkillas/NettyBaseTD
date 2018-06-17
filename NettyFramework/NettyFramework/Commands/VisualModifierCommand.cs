using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NettyFramework.Utils;

namespace NettyFramework.Commands
{
    public class VisualModifierCommand
    {
        public const short varR3I = 24;

        public const short var45R = 60;

        public const short var31b = 23;

        public const short varD1T = 1;

        public const short varHf = 13;

        public const short varz4u = 46;

        public const short var2g = 8;

        public const short var94I = 7;

        public const short varj19 = 42;

        public const short varC1k = 20;

        public const short var3P = 30;

        public const short vari2M = 52;

        public const short varj3l = 64;

        public const short INACTIVE = 14;

        public const short varo2y = 22;

        public const short varG1F = 53;

        public const short varoA = 67;

        public const short varF3o = 38;

        public const short varo3h = 45;

        public const short varZ3B = 55;

        public const short varB23 = 66;

        public const short varH32 = 59;

        public const short varK30 = 21;

        public const short varG1E = 3;

        public const short varO4T = 68;

        public const short varP3u = 49;

        public const short varVs = 41;

        public const short varx4P = 12;

        public const short varc21 = 58;

        public const short var84O = 33;

        public const short vare2N = 34;

        public const short SINGULARITY = 19;

        public const short varx1X = 0;

        public const short INVINCIBILITY = 32;

        public const short vare2W = 2;

        public const short varf1N = 57;

        public const short var24E = 35;

        public const short varX4t = 5;

        public const short varI5 = 9;

        public const short var1U = 27;

        public const short varD3F = 4;

        public const short var11v = 18;

        public const short vart2N = 63;

        public const short varu3N = 37;

        public const short var44f = 56;

        public const short varCc = 43;

        public const short varHT = 40;

        public const short var63T = 17;

        public const short varx3u = 11;

        public const short vark1b = 50;

        public const short varu3H = 51;

        public const short varr1k = 54;

        public const short varF2X = 44;

        public const short varZ1j = 62;

        public const short var83D = 25;

        public const short vard1h = 47;

        public const short varP39 = 61;

        public const short varR37 = 16;

        public const short var11d = 29;

        public const short varl33 = 39;

        public const short var25N = 15;

        public const short var41z = 10;

        public const short varK3W = 48;

        public const short varo2T = 26;

        public const short varA3k = 65;

        public const short vary4K = 31;

        public const short vart40 = 6;

        public const short var41x = 28;

        public const short varo34 = 36;

        public const short ID = 29545;

        public int attribute = 0;
        public string varl11 = "";
        public int userId = 0;
        public int count = 0;
        public bool activated = false;
        public short modifier = 0;

        public VisualModifierCommand(bool activated, short modifier, string varl11, int attribute, int userId, int count)
        {
            this.activated = activated;
            this.modifier = modifier;
            this.varl11 = varl11;
            this.attribute = attribute;
            this.userId = userId;
            this.count = count;
        }

        public byte[] write()
        {
            var cmd = new ByteArray(ID);
            cmd.writeBoolean(this.activated);
            cmd.writeInt(this.userId >> 16 | this.userId << 16);
            cmd.writeInt(this.attribute >> 5 | this.attribute << 27);
            cmd.writeShort(this.modifier);
            cmd.writeShort(-23947);
            cmd.writeInt(this.count << 7 | this.count >> 25);
            cmd.writeUTF(this.varl11);
            return cmd.Message.ToArray();
        }
    }
}
