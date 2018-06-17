using System;
using NettyBase.Game.world.objects;
using NettyBase.Networking.game_server;

namespace NettyBase.Game.controllers.implementable
{
    class Heal : IAbstractCharacter
    {
        public bool Healing = false;
        public int HealingId { get; set; }
        public int Amount { get; set; }
        public HealType HealType { get; set; }

        public Heal(AbstractCharacterController controller) : base(controller)
        {
            
        }

        private DateTime LastTick = new DateTime();
        public override void Tick()
        {
            if (LastTick.AddSeconds(1) < DateTime.Now)
            {
                Pulse();
                LastTick = DateTime.Now;
            }
        }

        public override void Stop()
        {
        }

        public void Pulse()
        {
            if (Healing)
            {
                var healedSession = World.StorageManager.GetGameSession(HealingId);
                if (healedSession == null || Amount == 0) return;
                healedSession.Player.Controller.Heal.Execute(Amount, Character.Id, HealType);
                //todo:fix
        //                GameClient.SendToPlayerView(Character, netty.commands.new_client.LegacyModule.write($"0|n|HEAL_RAY|{Character.Id}|{HealingId}"));
            }
        }

        public void Execute(int amount, int healerId = 0, HealType healType = HealType.HEALTH)
        {
            if (amount < 0)
                return;

            switch (healType)
            {
                case HealType.HEALTH:
                    if (Character.CurrentHealth + amount > Character.MaxHealth)
                        amount = Character.MaxHealth - Character.CurrentHealth;
                    Character.CurrentHealth += amount;
                    break;
                case HealType.SHIELD:
                    if (Character.CurrentShield + amount > Character.MaxShield)
                        amount = Character.MaxShield - Character.CurrentShield;
                    Character.CurrentShield += amount;
                    break;
            }

            if (Character is Player && healType == HealType.HEALTH)
            {
                //Packet.Builder.LegacyModule(World.StorageManager.GetGameSession(Character.Id), "0|A|HL|" + healerId + "|" + Character.Id + "|HPT|" + Character.CurrentHealth + "|" +
                //                                                                               amount);
                //todo: fix
            }
            else if (Character is Player && healType == HealType.SHIELD)
            {
                //Packet.Builder.LegacyModule(World.StorageManager.GetGameSession(Character.Id), "0|A|HL|" + healerId + "|" + Character.Id + "|SHD|" + Character.CurrentShield + "|" +
                //                                                                               amount);
                //todo: fix
            }
            Character.Updaters.Update();

        }
    }
}
