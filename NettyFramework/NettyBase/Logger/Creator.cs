﻿using System.IO;
using NettyBase.Properties;

namespace NettyBase.Logger
{
    class Creator
    {
        public static void InitializeSession()
        {
            Directory.CreateDirectory(Server.LOGGING_DIRECTORY + Program.SERVER_SESSION);
            Directory.CreateDirectory(Server.LOGGING_DIRECTORY + Program.SERVER_SESSION + "/debug");
            Directory.CreateDirectory(Server.LOGGING_DIRECTORY + Program.SERVER_SESSION + "/errors");
            Directory.CreateDirectory(Server.LOGGING_DIRECTORY + Program.SERVER_SESSION + "/executables");
            Directory.CreateDirectory(Server.LOGGING_DIRECTORY + Program.SERVER_SESSION + "/players");
            Directory.CreateDirectory(Server.LOGGING_DIRECTORY + Program.SERVER_SESSION + "/tasks");
        }

        public static void New(string filePath)
        {
            FileStream fileStream = null;
            if (!File.Exists(filePath))
                fileStream = File.Create(filePath);
            fileStream?.Dispose();
        }

        public static string ReformPath(string filePath)
        {
            return filePath.Replace("$SERVER_SESSION$", Program.SERVER_SESSION);
        }
    }
}
