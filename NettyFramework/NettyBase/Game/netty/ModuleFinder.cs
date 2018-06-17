using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NettyBase.Game.netty.handler;
using NettyBase.Networking.game_server;

namespace NettyBase.Game.netty
{
    static class ModuleFinder
    {
        public static Dictionary<short, IHandler> Modules = new Dictionary<short, IHandler> { };

        public static void FindModule(GameClient client, short id, byte[] bytes)
        {
            Console.WriteLine(id + " <<");
        }
    }
}
