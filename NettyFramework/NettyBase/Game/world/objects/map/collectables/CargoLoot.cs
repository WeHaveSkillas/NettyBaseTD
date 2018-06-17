using System;
using NettyBase.Game.world.objects.characters;

namespace NettyBase.Game.world.objects.map.collectables
{
    class CargoLoot : Collectable
    {
        private Character Killer { get; }
        private DropableRewards Rewards { get; }
        public CargoLoot(int id, string hash, Vector pos, DropableRewards dropableRewards, Character killer) : base(id,hash,Types.SHIP_LOOT, pos, killer.Spacemap, null)
        {
            Rewards = dropableRewards;
            Killer = killer;
            Temporary = true;
            DelayedDispose(15000);
        }
        
        protected override void Reward(Player player)
        {
            Console.WriteLine("TODO: Cargobox reward");
        }

        public override int GetTypeId(Character character)
        {
            if (Killer == character) return (int)Types.SHIP_LOOT;
            else return (int)Types.SHIP_LOOT_GRAY;
        }
    }
}
