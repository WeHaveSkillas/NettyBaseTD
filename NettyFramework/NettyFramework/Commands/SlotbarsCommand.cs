using System.Collections.Generic;
using NettyFramework.Utils;

namespace NettyFramework.Commands
{
    public class SlotbarsCommand
    {
        public const short ID = 10437;

        public static byte[] write(List<SlotbarCategoryModule> categories, string varnz, List<SlotbarQuickslotModule> slotBars)
        {
            var cmd = new ByteArray(ID);
            cmd.writeInt(categories.Count);
            foreach (var c in categories)
            {
                cmd.AddBytes(c.write());
            }
            cmd.writeShort(7085);
            cmd.writeUTF(varnz);
            cmd.writeInt(slotBars.Count);
            foreach (var c in slotBars)
            {
                cmd.AddBytes(c.write());
            }
            return cmd.ToByteArray();
        }
    }
}
