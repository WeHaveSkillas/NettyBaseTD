using System;
using NettyBase.Game;
using NettyBase.Logger.types;
using NettyBase.Properties;
using NettyBase.Utils;

namespace NettyBase.Main
{
    public class ConsoleMonitor
    {
        public static void Check()
        {
            if (ExceptionLog.ERRORS_RECORDED > 100000 && !Properties.Server.DEBUG)
                Program.CloseForMaintenance();
        }

        public static void Update()
        {
            if (!Program.ServerUp) return;
            var oldCursorPosX = Console.CursorLeft;
            var oldCursorPosY = Console.CursorTop;

            if (oldCursorPosY > 50)
            {
                Draw.Logo(true);
                oldCursorPosY = 19;
            }

            var oldColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(0, 17);
            Console.WriteLine("\r Version = {0} // Errors = {1} / Online = {2}", Program.GetVersion(), ExceptionLog.ERRORS_RECORDED, "W:" + World.StorageManager.GameSessions.Count);
            Console.SetCursorPosition(oldCursorPosX, oldCursorPosY);
            Console.ForegroundColor = oldColor;
            Console.Title = "Unknown Universe / " + Program.GetVersion() + " / " + (DateTime.Now - Server.RUNTIME).ToString(@"dd\.hh\:mm\:ss");
        }
    }
}