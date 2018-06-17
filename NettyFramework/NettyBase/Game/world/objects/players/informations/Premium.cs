using System;

namespace NettyBase.Game.world.objects.players.informations
{
    class Premium
    {
        public DateTime ExpiryDate { get; set; }

        public bool Active => ExpiryDate > DateTime.Now;

        public void Login(GameSession gameSession)
        {
            if (Active)
            {
                //todo: fix

                //Packet.Builder.LegacyModule(gameSession,
                //    $"0|A|STD|Premium active until {ExpiryDate.ToLongDateString()}.\nThanks for supporting the server!");
            }
        }

        private bool MessageSent = false;
        public void Update(Player player)
        {
            //TODO: Fix live premium update.
            //if (player != null)
            //{
            //    var gameSession = player.GetGameSession();
            //    if (gameSession == null) return;
            //    Packet.Builder.QuickSlotPremiumCommand(player.GetGameSession());
            //    if (Active)
            //    {
            //        Packet.Builder.LegacyModule(player.GetGameSession(),
            //            $"0|A|STD|Premium active until {ExpiryDate.ToLongDateString()}.\nThanks for supporting the server!");
            //        MessageSent = true;
            //    }
            //    else
            //        Packet.Builder.LegacyModule(player.GetGameSession(),
            //            $"0|A|STD|Your Premium expired on {ExpiryDate.ToLongDateString()}.\nIf you wish to support the server once again, head over to the Premium page.");
            //}
        }
    }
}
