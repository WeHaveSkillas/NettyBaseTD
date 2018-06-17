﻿using System;
using NettyBase.Game;
using NettyBase.Game.world.objects;

namespace NettyBase.Main.commands
{
    class DestroyCommand : Command
    {
        public DestroyCommand() : base("destroy", "Destroy command")
        {

        }

        public override void Execute(string[] args = null)
        {
            try
            {
                var whomst = args[1];
                var targetId = int.Parse(args[2]);

                switch (whomst)
                {
                    case "npcs":
                        foreach (var entity in World.StorageManager.Spacemaps[targetId].Entities)
                        {
                            if (entity.Value is Npc)
                                entity.Value.Controller.Destruction.Kill();
                        }
                        break;
                    case "id":
                        World.StorageManager.GetGameSession(targetId)?.Player.Controller.Destruction.Kill();
                        break;
                    case "selected":
                        World.StorageManager.GetGameSession(targetId)?.Player.Selected?.Destroy();
                        break;
                    case "poi":
                        World.StorageManager.GetGameSession(targetId)?.Player.Spacemap.POIs.Remove(args[3]);
                        break;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid args");
            }
        }
    }

}
