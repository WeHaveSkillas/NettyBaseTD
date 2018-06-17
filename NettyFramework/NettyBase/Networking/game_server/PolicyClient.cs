using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NettyBase.Networking.game_server
{
    class PolicyClient
    {
        private XSocket XSocket;

        public PolicyClient(XSocket gameSocket)
        {
            XSocket = gameSocket;
            XSocket.OnReceive += XSocketOnOnReceive;
            XSocket.Read(true);
        }

        private void XSocketOnOnReceive(object sender, EventArgs eventArgs)
        {
            var packet = ((StringArgs)eventArgs).Packet;

            const string policyPacket = "<?xml version=\"1.0\"?>\r\n" +
                                        "<!DOCTYPE cross-domain-policy SYSTEM \"/xml/dtds/cross-domain-policy.dtd\">\r\n" +
                                        "<cross-domain-policy>\r\n" +
                                        "<allow-access-from domain=\"*\" to-ports=\"*\" />\r\n" +
                                        "</cross-domain-policy>";

            if (packet.StartsWith("<policy-file-request/>"))
                XSocket.Write(policyPacket);
            else
                Console.WriteLine("Errorino with policy request: {0}", packet);
        }
    }
}
