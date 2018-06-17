using System.Collections.Generic;
using NettyFramework.Utils;

namespace NettyFramework.Commands
{
    public class SlotbarCategoryModule
    {
        public const short ID = 16810;

        public string name;
        public List<SlotbarCategoryItemModule> items;

        public SlotbarCategoryModule(string name, List<SlotbarCategoryItemModule> items)
        {
            this.name = name;
            this.items = items;
        }

        public byte[] write()
        {
            var cmd = new ByteArray(ID);
            cmd.writeInt(items.Count);
            foreach (var c in items)
            {
                cmd.AddBytes(c.write());
            }
            cmd.writeShort(8139);
            cmd.writeUTF(name);
            return cmd.Message.ToArray();
        }

    }
}
