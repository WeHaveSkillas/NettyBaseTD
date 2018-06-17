using System.Collections.Generic;
using NettyBase.Game.world.objects.map;
using NettyBase.Game.world.objects.map.collectables;
using NettyBase.Game.world.objects.pets;

namespace NettyBase.Game.controllers.pet.gears
{
    class AutoResourceCollectionGear : Gear
    {
        public override List<int> OptionalParams => new List<int>{0};

        public AutoResourceCollectionGear(PetController controller) : base(controller, true, 3)
        {
            Type = GearType.AUTO_RESOURCE_COLLECTION;
        }

        public override void Activate()
        {
        }

        public OreTypes OreTypeSelected;
        public override void Check()
        {
            if (SelectedOre != null) CollectOre();
            else FindOre();
        }

        private Ore SelectedOre;

        private void FindOre()
        {

        }

        private void CollectOre()
        {

        }

        public override void End(bool shutdown = false)
        {
        }
    }
}
