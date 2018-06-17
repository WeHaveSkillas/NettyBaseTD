using System;
using System.Threading.Tasks;

namespace NettyBase.Game.world.objects.players.extra
{
    abstract class Tech : PlayerBaseClass
    {
        public bool Enabled { get; set; }
        public bool Active { get; set; }

        public int Count { get; set; }
        public DateTime TimeFinish { get; set; }

        protected Tech(Player player) : base(player) { }

        public int TimeLeft => Math.Abs((TimeFinish - DateTime.Now).Seconds);

        public abstract void Tick();

        public abstract void execute();

        public virtual void ThreadUpdate() { }

        public void Start()
        {
            Task.Factory.StartNew(() =>
            {
                while (Active && Player.Controller.Active)
                {
                    System.Threading.Thread.Sleep(1000);
                    ThreadUpdate();
                }
            });
        }

        public int GetStatus()
        {
            if (Active) return 2;
            if (Enabled) return 1;
            return 0;
        }
    }
}
