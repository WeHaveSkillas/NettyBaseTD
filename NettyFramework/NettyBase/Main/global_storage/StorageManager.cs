﻿using System.Collections.Generic;
using System.Linq;
using NettyBase.Main.objects;

namespace NettyBase.Main.global_storage
{
    class StorageManager
    {
        public Dictionary<int, Clan> Clans = new Dictionary<int, Clan>();

        public Clan GetClan(int id)
        {
            if (Clans.ContainsKey(id))
                return Clans[id];
            return Global.QueryManager.GetClan(id);
        }

        public Clan GetClan(string tag)
        {
            return Clans.Values.FirstOrDefault(x => x.Tag == tag);
        }
    }
}