﻿using System;
using Newtonsoft.Json;

namespace NettyBase.Game.world.objects.map.objects
{
    class PortalBase
    {
        public int Id { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int newX { get; set; }
        public int newY { get; set; }
        public int Map { get; set; }

        public PortalBase(int Id, int x, int y, int newX, int newY, int Map)
        {
            this.Id = Id;
            this.x = x;
            this.y = y;
            this.newX = newX;
            this.newY = newY;
            this.Map = Map;
        }
    }

    class Jumpgate : Object, IClickable
    {
        public Faction Faction { get; set; }

        public int LevelRequired { get; set; }

        public Vector Destination { get; set; }

        public int DestinationMapId { get; set; }

        public int DestinationVirtualWorldId { get; set; }

        public bool Visible { get; set; }
        
        public int FactionScrap { get; set; }
        
        public int RequiredLevel { get; set; }

        public int Gfx { get; set; }

        public bool Working { get; set; }

        public Player Owner { get; set; }

        public Jumpgate(int id, Faction faction, Vector pos, Spacemap map, Vector destinationPos, int destinationMapId, bool visible, int factionScrap, int requiredLevel, int gfx) : base(id, pos, map)
        {
            Faction = faction;
            Destination = destinationPos;
            DestinationMapId = destinationMapId;
            Visible = visible;
            FactionScrap = factionScrap;
            RequiredLevel = requiredLevel;
            Gfx = gfx;
            Working = true;
            DestinationVirtualWorldId = 0;
        }
       
        public override void execute(Character character)
        {
        }

        public virtual void click(Character character)
        {
            var player = character as Player;
            player?.Controller.Miscs.Jump(DestinationMapId, Destination, Id, DestinationVirtualWorldId);
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public string ToPacket()
        {
            return "0|p|" + Id + "|" + (int)Faction + "|" + Gfx + "|" + Position.X + "|" + Position.Y + "|" + Convert.ToInt32(Visible) + "|" + FactionScrap;
        }
    }
}
