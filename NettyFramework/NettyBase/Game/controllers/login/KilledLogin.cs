using NettyBase.Game.world;
using NettyBase.Game.world.objects;
using NettyBase.Game.world.objects.players.killscreen;

namespace NettyBase.Game.controllers.login
{
    class KilledLogin : ILogin
    {
        public KilledLogin(GameSession gameSession) : base(gameSession)
        {
            gameSession.Player.EntityState = EntityStates.DEAD;
        }

        public override void Execute()
        {
            SendSettings();
            SendLegacy();
            //TODO: Fix;
            //Packet.Builder.KillScreenCommand(GameSession, Killscreen.Load(GameSession.Player));
        }
    }
}
