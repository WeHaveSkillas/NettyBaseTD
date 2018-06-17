using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NettyFramework.Utils;

namespace NettyFramework.Commands
{
    public class WindowsCommand
    {
        public const short ID = 12242;
        public static byte[] write(List<commandKn> slotbars)
        {
            var cmd = new ByteArray(ID);
            cmd.writeShort(14628);
            cmd.writeInt(slotbars.Count);
            foreach (commandKn c in slotbars)
            {
                cmd.AddBytes(c.write());
            }
            cmd.writeShort(32289);
            return cmd.ToByteArray();
        }
    }
}
