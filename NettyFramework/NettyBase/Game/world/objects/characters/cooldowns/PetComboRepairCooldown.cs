using System;
using System.Collections.Generic;

namespace NettyBase.Game.world.objects.characters.cooldowns
{
    class PetComboRepairCooldown : Cooldown
    {
        public PetComboRepairCooldown() : base(DateTime.Now, DateTime.Now.AddSeconds(30))
        {
        }

        public override void OnStart(Character character)
        {
            base.OnStart(character);
        }

        public override void OnFinish(Character character)
        {
            var player = character as Player;
            if (player != null)
            {
                //Packet.Builder.PetBuffCommand(player.GetGameSession(), 1, 6, new List<int>());
            }
        }


        public override void Send(GameSession gameSession)
        {
            //Packet.Builder.PetBuffCommand(gameSession, 0, 6, new List<int>{ 30 });
        }
    }
}
