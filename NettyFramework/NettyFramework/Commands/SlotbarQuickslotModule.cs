using System.Collections.Generic;
using NettyFramework.Utils;

namespace NettyFramework.Commands
{
    public class SlotbarQuickslotModule
    {
        public const short ID = 2545;

        public string slotBarId;
        public List<SlotbarQuickslotItem> var2Z;
        public string position;
        public string varW2e;
        public bool visible;

        public SlotbarQuickslotModule(string slotBarId, List<SlotbarQuickslotItem> var2Z, string position, string varW2E, bool visible)
        {
            this.slotBarId = slotBarId;
            this.var2Z = var2Z;
            this.position = position;
            varW2e = varW2E;
            this.visible = visible;
        }

        public byte[] write()
        {
            var cmd = new ByteArray(ID);
            cmd.writeUTF(position);
            cmd.writeShort(-31484);
            cmd.writeInt(var2Z.Count);
            foreach (var c in var2Z)
            {
                cmd.AddBytes(c.write());
            }
            cmd.writeUTF(slotBarId);
            cmd.writeBoolean(visible);
            cmd.writeShort(-8177);
            cmd.writeUTF(varW2e);
            return cmd.Message.ToArray();
        }
    }
}
