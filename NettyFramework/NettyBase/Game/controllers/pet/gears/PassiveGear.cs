using NettyBase.Game.world.objects.pets;

namespace NettyBase.Game.controllers.pet.gears
{
    class PassiveGear : Gear
    {
        internal PassiveGear(PetController controller) : base(controller, true, 1)
        {
            Type = GearType.PASSIVE;
        }

        public override void Activate()
        {

        }

        public override void Check()
        {
            Follow(baseController.Pet.GetOwner());
        }

        public override void End(bool shutdown = false)
        {
        }
    }
}
