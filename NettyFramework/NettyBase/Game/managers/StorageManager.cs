using System.Collections.Generic;
using NettyBase.Game.world;
using NettyBase.Game.world.objects;
using NettyBase.Game.world.objects.characters;
using NettyBase.Game.world.objects.events;
using NettyBase.Game.world.objects.map;
using NettyBase.Game.world.objects.map.objects.assets;
using NettyBase.Game.world.objects.players;
using NettyBase.Game.world.objects.players.informations;

namespace NettyBase.Game.managers
{
    class StorageManager
    {
        public Dictionary<int, GameSession> GameSessions = new Dictionary<int, GameSession>();
        public Dictionary<int, Ship> Ships = new Dictionary<int, Ship>();
        public Dictionary<int, Spacemap> Spacemaps = new Dictionary<int, Spacemap>();
        public List<Ore> OrePrices = new List<Ore>();
        public Levels Levels = new Levels();
        public Dictionary<int, Title> Titles = new Dictionary<int, Title>();
        public List<Group> Groups = new List<Group>();
        public Dictionary<int, GameEvent> Events = new Dictionary<int, GameEvent>();
        public Dictionary<int, Quest> Quests = new Dictionary<int, Quest>();

        public Dictionary<int, ClanBattleStation> ClanBattleStations = new Dictionary<int, ClanBattleStation>();
        public Dictionary<int, QuestGiver> QuestGivers = new Dictionary<int, QuestGiver>();

        public GameSession GetGameSession(int userId)
        {
            return GameSessions.ContainsKey(userId) ? GameSessions[userId] : null;
        }
    }
}
