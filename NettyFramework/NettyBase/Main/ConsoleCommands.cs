﻿using System;
using System.Collections.Generic;
using NettyBase.Main.commands;

namespace NettyBase.Main
{
    public class ConsoleCommands
    {
        public static Dictionary<string, Command> Commands = new Dictionary<string, Command>();

        public static void Add()
        {
            Commands.Add("help", new HelpCommand());
            Commands.Add("clean", new CleanCommand());
            Commands.Add("player", new PlayerCommand());
            Commands.Add("set", new SetCommand());
            Commands.Add("debug", new DebugCommand());
            Commands.Add("destroy", new DestroyCommand());
            Commands.Add("start", new StartCommand());
            Commands.Add("info", new InfoCommand());
            Commands.Add("create", new CreateCommand());
        }

        public static void Handle(string txt)
        {
            var packet = txt.Replace("/", "");
            if (packet.Contains(" "))
            {
                var splitted = packet.Split(' ');
                if (Commands.ContainsKey(splitted[0]))
                    Commands[splitted[0]].Execute(splitted);
            }
            else if (Commands.ContainsKey(packet))
                Commands[packet].Execute();
            else Console.WriteLine("ERROR: Command doesn't exist");
        }
    }
}