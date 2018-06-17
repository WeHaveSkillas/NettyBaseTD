using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NettyBase.Game.controllers;
using NettyBase.Game.world;
using NettyBase.Game.world.objects;
using NettyBase.Networking.game_server;
using NettyFramework.Commands;
using NettyFramework.Commands.requests;

namespace NettyBase.Game.netty.handler
{
    class LoginHandler
    {
        private GameClient _client;
        private LoginRequest _request;

        private Player Player;

        private GameSession GameSession;

        public LoginHandler(GameClient client, byte[] bytes)
        {
            _request = new LoginRequest();
            _request.readCommand(bytes);
            _client = client;
        }

        #region handling methods
        public void execute()
        {
            Console.WriteLine(_request.userID.ToString());
            _client.UserId = _request.userID;
            var userId = _request.userID;
            var tempSession = World.StorageManager.GetGameSession(userId);
            if (tempSession != null && (tempSession.InProcessOfReconection || tempSession.InProcessOfDisconnection))
            {
                Player = tempSession.Player;
                World.StorageManager.GameSessions.Remove(userId);
            }

            Player = GetAccount(userId);
            if (Player != null) GameSession = CreateSession(_client, Player);
            else
            {
                Console.WriteLine("Failed loading user ship / ShipInitializationHandler ERROR");
                return;
            }

            Player.Log.Write($"Session received: {_request.sessionID}->Database session: {Player.SessionId}");
            if (Player.SessionId != _request.sessionID)
            {
                ExecuteWrongSession();
                return; // Wrong session ID
            }

            prepare();
        }

        private void prepare()
        {
            if (GameSession == null) return;

            if (!World.StorageManager.GameSessions.ContainsKey(Player.Id))
                World.StorageManager.GameSessions.Add(Player.Id, GameSession);
            else
            {
                World.StorageManager.GameSessions[Player.Id].Disconnect(GameSession.DisconnectionType.NORMAL);
                World.StorageManager.GameSessions[Player.Id] = GameSession;
            }

            LoginController.Initiate(GameSession);

        }
#endregion

        private Player GetAccount(int userId)
        {
            if (Player != null) return Player;
            return World.DatabaseManager.GetAccount(userId);
        }

        private void ExecuteWrongSession()
        {
            Console.WriteLine($"{GameSession.Client.IPAddress} tried breaching into {GameSession.Client.UserId}'s account");
            Player.Log.Write($"Breach attempt by {GameSession.Client.IPAddress}");
        }

        private GameSession CreateSession(GameClient client, Player player)
        {
            return new GameSession(player)
            {
                Client = client,
                LastActiveTime = DateTime.Now
            };
        }
    }
}
