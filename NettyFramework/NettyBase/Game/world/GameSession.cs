using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NettyBase.Game.netty.builder;
using NettyBase.Game.world.objects;
using NettyBase.Main;
using NettyBase.Main.interfaces;
using NettyBase.Networking.game_server;

namespace NettyBase.Game.world
{
    class GameSession : ITick
    {
        public enum DisconnectionType
        {
            NORMAL,
            INACTIVITY,
            ADMIN,
            SOCKET_CLOSED,
            ERROR
        }

        public PacketBuilder Builder { get; }


        public Player Player { get; set; }

        public GameClient Client { get; set; }

        public DateTime LastActiveTime = new DateTime(2016, 12, 15, 0, 0, 0);

        public bool InProcessOfReconection = false;

        public bool InProcessOfDisconnection = false;

        public DateTime EstDisconnectionTime = new DateTime();

        public GameSession(Player player)
        {
            Player = player;
            Builder = new PacketBuilder(this);
            Global.TickManager.Add(this);
        }

        public void Tick()
        {
            if (LastActiveTime >= DateTime.Now.AddMinutes(5))
                Disconnect(DisconnectionType.INACTIVITY);
            if (EstDisconnectionTime < DateTime.Now && InProcessOfDisconnection)
                Disconnect(DisconnectionType.NORMAL);
        }

        public void Relog(Spacemap spacemap = null, Vector pos = null)
        {
            InProcessOfReconection = true;
            PrepareForDisconnect(); // preparation
            spacemap = spacemap ?? Player.Spacemap;
            pos = pos ?? Player.Position;
            Player.Spacemap = spacemap;
            Player.ChangePosition(pos);
            Disconnect(); // closing the socket
            //Player.Save();
        }

        private void PrepareForDisconnect()
        {
            Player.Save();
            Player.Controller.Attack.Attacking = false;
            Player.Controller.Exit();
            Player.Controller.Destruction.Remove();

            Global.TickManager.Remove(this);
            Global.TickManager.Remove(Player);
        }

        public void Kick()
        {
            PrepareForDisconnect();
            Disconnect();
        }

        /// <summary>
        /// No preparations, just close the socket
        /// </summary>
        public void Disconnect()
        {
            Client.Disconnect();
        }

        public void Disconnect(DisconnectionType dcType)
        {
            InProcessOfDisconnection = true;
            if (dcType == DisconnectionType.SOCKET_CLOSED)
            {
                EstDisconnectionTime = DateTime.Now.AddSeconds(30);
                return;
            }
            PrepareForDisconnect();
            Player.Log.Write($"User disconnected (Disconnection Type: {dcType})");
            //TODO: fix
            //Packet.Builder.LegacyModule(this, "ERR|2");
            Client.Disconnect();
            World.StorageManager.GameSessions.Remove(Player.Id);
            InProcessOfDisconnection = false;
            GC.Collect();
        }
    }
}
