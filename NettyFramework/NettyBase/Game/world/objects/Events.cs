using System;

namespace NettyBase.Game.world.objects
{
    class CharacterArgs : EventArgs
    {
        public Character Character { get; }
        public CharacterArgs(Character character)
        {
            Character = character;
        }
    }
}
