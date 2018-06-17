using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using NettyBase.Game;
using NettyBase.Game.netty;
using NettyBase.Game.world;
using NettyBase.Game.world.objects;
using NettyBase.Main.commands;
using NettyBase.Utils;

namespace NettyBase.Networking.game_server
{
    class GameClient
    {
        private XSocket XSocket;
        public int UserId { get; set; }

        public IPAddress IPAddress => XSocket.RemoteHost;

        public GameClient(XSocket gameSocket)
        {
            XSocket = gameSocket;
            XSocket.OnReceive += XSocketOnOnReceive;
            XSocket.ConnectionClosedEvent += XSocketOnConnectionClosedEvent;
            XSocket.Read();
        }

        private void XSocketOnConnectionClosedEvent(object sender, EventArgs eventArgs)
        {
            var gameSess = World.StorageManager.GetGameSession(UserId); // TODO: Fix regular connection drop - player still stays in range even after disconnection
            if (!gameSess.InProcessOfReconection)
                gameSess.Disconnect(GameSession.DisconnectionType.SOCKET_CLOSED);
        }

        private void XSocketOnOnReceive(object sender, EventArgs eventArgs)
        {
            var bytes = (ByteArrayArgs)eventArgs;
            var packet = Encoding.UTF8.GetString(bytes.ByteArray);

            if (packet.StartsWith("dialog"))
            {
                //TODO: fix
            //    Packet.Handler.DialogLookUp(packet);
            }
            else
            {
                var parsedCommand = new ByteParser(bytes.ByteArray);
                ModuleFinder.FindModule(this, parsedCommand.CMD_ID, bytes.ByteArray);
            }
        }

        public void Send(byte[] bytes)
        {
            try
            {
                var gameSession = World.StorageManager.GetGameSession(UserId);
                if (gameSession != null)
                {
                    if (gameSession.InProcessOfReconection || gameSession.InProcessOfDisconnection) return;
                    if (gameSession.Player.Controller != null)
                        gameSession.LastActiveTime = DateTime.Now;

                    XSocket.Write(bytes);
                    //Console.WriteLine(Out.GetCaller() + $" ({DateTime.Now})");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("->" + Out.GetCaller());
                //new ExceptionLog("socket", "Unable to send packet / Connected?", e);
            }
        }
        public void Send(string packet)
        {
            XSocket.Write(packet);
        }

        public static void SendRangePacket(Character character, byte[] bytes, bool sendCharacter = false)
        {
            if (character == null) return;
            try
            {
                foreach (var entry in character.Spacemap.Entities)
                {
                    var entity = entry.Value as Player;
                    if (entity == null) continue;

                    if (character.InRange(entity) && entity != character)
                    {
                        entity.GetGameSession()?.Client.Send(bytes);
                    }
                }

                if (sendCharacter && character is Player)
                {
                    var player = (Player)character;
                    player.GetGameSession()?.Client.Send(bytes);
                }
            }
            catch (Exception e)
            {
                Out.WriteLine("Something went wrong sending a range packet.", "ERROR", ConsoleColor.Red);
                Debug.WriteLine(e.Message, "Debug Error");
            }
        }

        public static void SendPacketSelected(Character character, byte[] bytes)
        {
            try
            {
                foreach (var entry in character.Spacemap.Entities)
                {
                    var entity = entry.Value;

                    if (entity is Player && entity.Selected != null)
                    {
                        if (entity.Selected.Id == character.Id)
                        {
                            var entitySession = World.StorageManager.GetGameSession(entity.Id);
                            if (entitySession != null)
                                entitySession.Client.Send(bytes);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Out.WriteLine("Something went wrong sending a range packet.", "ERROR", ConsoleColor.Red);
                Debug.WriteLine(e.Message, "Debug Error");
            }
        }

        public static void SendToSpacemap(Spacemap spacemap, byte[] bytes)
        {
            try
            {
                foreach (var entry in spacemap.Entities)
                {
                    var entity = entry.Value as Player;
                    if (entity != null && (entity.Spacemap != null || entity.Position != null))
                    {
                        World.StorageManager.GameSessions[entity.Id]?.Client.Send(bytes);
                    }
                }
            }
            catch (Exception e)
            {
                Out.WriteLine("Something went wrong sending a spacemap packet.", "ERROR", ConsoleColor.Red);
                Debug.WriteLine(e.Message, "Debug Error");

            }
        }


        public void Disconnect()
        {
            try
            {
                XSocket.Close();
            }
            catch (Exception)
            {
                Out.WriteLine("Error disconnecting user from Game", "GAME", ConsoleColor.DarkRed);
            }
        }

    }
}
