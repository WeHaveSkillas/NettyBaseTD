using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NettyFramework.Utils;

namespace NettyFramework.Commands.requests
{
    public class LoginRequest : IRequestedCommand
    {
        public const short ID = 26758;

        public int factionID;
        public string sessionID;
        public int instanceId;
        public int userID;
        public string version;

        public void readCommand(byte[] bytes)
        {
            var parser = new ByteParser(bytes);
            factionID = parser.readShort();
            sessionID = parser.readUTF();
            this.instanceId = parser.readInt();
            this.instanceId = this.instanceId >> 12 | this.instanceId << 20;
            this.version = parser.readUTF();
            this.userID = parser.readInt();
            this.userID = this.userID >> 11 | this.userID << 21;
        }
    }
}
