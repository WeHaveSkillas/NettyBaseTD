using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NettyBase.Game.netty.handler;
using NettyBase.Networking.game_server;
using NettyFramework.Commands.requests;

namespace NettyBase.Game.netty
{
    static class ModuleFinder
    {
        public static Dictionary<short, IHandler> Modules = new Dictionary<short, IHandler> { };

        public static void FindModule(GameClient client, short id, byte[] bytes)
        {
            if (Modules.ContainsKey(id))
            {
                var session = World.StorageManager.GetGameSession(client.UserId);
                if (session != null)
                    Modules[id].execute(session, bytes);
            }
            else if (id == LoginRequest.ID)
            {
                new LoginHandler(client, bytes).execute();
            }
            else Console.WriteLine($"Unhandled module [{id}]");
        }
    }
}
