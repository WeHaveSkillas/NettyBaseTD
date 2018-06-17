using System;

namespace NettyBase.Game.world.objects.map
{
    abstract class Mine : Object
    {
        public string Hash { get; set; }

        public virtual bool PulseActive => true;

        public virtual int MineType => -1;

        protected Mine(int id, string hash, Vector pos, Spacemap map) : base(id, pos, map)
        {
            Hash = hash;
        }

        public override void execute(Character character)
        {
            Console.WriteLine("bombing.");
        }
    }
}
