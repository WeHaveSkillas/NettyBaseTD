using System;
using System.Threading.Tasks;
using NettyBase.Game;
using NettyBase.Logger.types;
using NettyBase.Main.global_managers;
using NettyBase.Main.global_storage;
using NettyBase.Main.objects;
using NettyBase.Networking;
using NettyBase.Utils;
using Random = System.Random;

namespace NettyBase.Main
{
    static class Global
    {
        public static QueryManager QueryManager = new QueryManager();
        public static TickManager TickManager = new TickManager();
        public static StorageManager StorageManager = new StorageManager();
        public static CronjobManager CronjobManager = new CronjobManager();
        public static DebugLog Log = new DebugLog("global");

        public static State State = State.LOADING;

        public static void Start()
        {
            InitiateGlobalQueries();
            InitiatePolicy();
            InitiateChat();
            InitiateGame();
            InitiateRandomResetTimer();
            //TODO -> ACP InitiateSocketty();
            State = State.READY;
            CronjobManager.Initiate();
            var task = new Task(() => TickManager.Tick(), TaskCreationOptions.LongRunning);
            task.Start();
        }

        static void InitiateGlobalQueries()
        {
            QueryManager.Load();
            Log.Write("Global-Queries loaded!");
        }

        static void InitiateChat()
        {
            //Chat.Chat.Init();
            //new Server(Server.CHAT_PORT);

            //Out.WriteLine("Chat-Server started successfully and DB loaded!", "SUCCESS", ConsoleColor.DarkGreen);
            //Log.Write("Chat-Server started.");
        }

        static void InitiatePolicy()
        {
            new Server(Server.POLICY_PORT);

            Out.WriteLine("Policy-Server started successfully!", "SUCCESS", ConsoleColor.DarkGreen);
            Log.Write("Policy-Server started.");
        }
        
        static void InitiateGame()
        {
            World.InitiateManagers();
            new Server(Server.GAME_PORT);

            Out.WriteLine("Game-Server started successfully and DB loaded!", "SUCCESS", ConsoleColor.DarkGreen);
            Log.Write("Game-Server started.");
        }

        static void InitiateRandomResetTimer()
        {
            TickManager.Add(new Utils.Random());
        }

        public static void SaveAll()
        {
            QueryManager.SaveAll();
        }
    }
}
