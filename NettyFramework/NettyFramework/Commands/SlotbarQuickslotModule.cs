using System.Collections.Generic;
using NettyFramework.Utils;

namespace NettyFramework.Commands
{
    public class SlotbarQuickslotModule
    {
        public const short ID = 28968;

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
            cmd.writeInt(var2Z.Count);
            foreach (var c in var2Z)
            {
                cmd.AddBytes(c.write());
            }
            cmd.writeUTF(this.slotBarId);
            cmd.writeUTF(position);
            cmd.writeShort(-26626);
            cmd.writeUTF(varW2e);
            cmd.writeBoolean(this.visible);
            return cmd.Message.ToArray();
        }
    }
}
