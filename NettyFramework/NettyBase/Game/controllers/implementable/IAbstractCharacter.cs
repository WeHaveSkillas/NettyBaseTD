using NettyBase.Game.world.objects;

namespace NettyBase.Game.controllers.implementable
{
    abstract class IAbstractCharacter
    {
        public AbstractCharacterController Controller { get; }

        internal Character Character => Controller.Character;

        public IAbstractCharacter(AbstractCharacterController controller)
        {
            Controller = controller;
        }

        public abstract void Tick();

        public abstract void Stop();
    }
}
