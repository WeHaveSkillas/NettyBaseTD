using System;
using System.Text;
using NettyBase.Game;

namespace NettyBase.Main.commands
{
    class InfoCommand : Command
    {
        public InfoCommand() : base("info", "Info about server")
        {
        }

        public override void Execute(string[] args = null)
        {
            if (args == null || args.Length < 2)
                return;
            switch (args[1])
            {
                case "players":
                    StringBuilder worldSessions = new StringBuilder();
                    foreach (var worldSession in World.StorageManager.GameSessions)
                        worldSessions.Append($"{worldSession.Key}:{worldSession.Value.Player.Name} ");
                    Console.WriteLine($"World sessions ({World.StorageManager.GameSessions.Count}): {worldSessions}");
                    break;
                case "mysql":
                    break;
            }
        }
    }
}
