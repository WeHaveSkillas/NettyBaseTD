using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NettyBase.Game.world;

namespace NettyBase.Game
{
    class World
    {
        public static ConcurrentDictionary<int, GameSession> Sessions = new ConcurrentDictionary<int, GameSession>();
    }
}
