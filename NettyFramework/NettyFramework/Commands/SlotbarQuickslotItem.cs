using NettyFramework.Utils;

namespace NettyFramework.Commands
{
    public class SlotbarQuickslotItem
    {
        public const short ID = 32562;

        public int slotId;
        public string lootId;

        public SlotbarQuickslotItem(int slotId, string lootId)
        {
            this.slotId = slotId;
            this.lootId = lootId;
        }

        public byte[] write()
        {
            var cmd = new ByteArray(ID);
            cmd.writeInt(this.slotId << 16 | this.slotId >> 16);
            cmd.writeUTF(lootId);
            return cmd.Message.ToArray();
        }

    }
}
