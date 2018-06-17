namespace NettyBase.Game.controllers.events
{
    interface IEvent
    {
        void Tick();
        void Start();
        void Finish();
    }
}
