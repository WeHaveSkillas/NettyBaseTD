using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NettyFramework.utils;

namespace NettyFramework.Commands.requests
{
    public class LoginRequest : IRequestedCommand
    {
        public const short ID = 23263;

        public int factionID;
        public string sessionID;
        public int instanceId;
        public int userID;
        public string version;

        public void readCommand(byte[] bytes)
        {
            var parser = new ByteParser(bytes);
            factionID = parser.readShort();
            parser.readShort();
            sessionID = parser.readUTF();
            instanceId = parser.readInt();
            instanceId = instanceId >> 6 | instanceId << 26;
            userID = parser.readInt();
            userID = userID >> 16 | userID << 16;
            version = parser.readUTF();
            parser.readShort();
        }
    }
}
