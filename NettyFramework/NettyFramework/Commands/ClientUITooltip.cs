using System.Collections.Generic;
using NettyFramework.Utils;

namespace NettyFramework.Commands
{
    public class ClientUITooltip
    {
        public const short ID = 11872;

        private List<ClientUITooltipTextFormat> textFormat;

        public ClientUITooltip(List<ClientUITooltipTextFormat> textFormat)
        {
            this.textFormat = textFormat;
        }

        public byte[] write()
        {
            var cmd = new ByteArray(ID);
            cmd.writeInt(textFormat.Count);
            foreach (var format in textFormat)
            {
                cmd.AddBytes(format.write());
            }
            return cmd.Message.ToArray();
        }
    }
}
