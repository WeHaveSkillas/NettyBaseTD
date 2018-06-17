namespace NettyBase.Game.controllers.npc
{
    interface INpc
    {
        void Tick();
        void Active();
        void Inactive();
        void Paused();
        void Exit();
    }
}
