using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NettyFramework.Utils;

namespace NettyFramework.Commands
{
    public class WindowButtonModule
    {
        public const short ID = 28947;

        public ClientUITooltip tooltip;

        public bool visible = false;

        public string itemId = "";

        public WindowButtonModule(string itemId, bool visible, ClientUITooltip tooltip)
        {
            this.visible = visible;
            this.itemId = itemId;
            this.tooltip = tooltip;
        }

        public byte[] write()
        {
            var cmd = new ByteArray(ID);
            cmd.writeShort(13273);
            cmd.AddBytes(tooltip.write());
            cmd.writeBoolean(this.visible);
            cmd.writeUTF(this.itemId);
            return cmd.Message.ToArray();
        }
    }
}
