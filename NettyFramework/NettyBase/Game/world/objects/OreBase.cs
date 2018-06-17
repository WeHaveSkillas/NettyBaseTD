﻿namespace NettyBase.Game.world.objects
{
    abstract class OreBase
    {
        public int Prometium { get; set; }
        public int Endurium { get; set; }
        public int Terbium { get; set; }
        public int Prometid { get; set; }
        public int Duranium { get; set; }
        public int Xenomit { get; set; }
        public int Promerium { get; set; }
        public int Seprom { get; set; }

        public OreBase(int Prometium, int Endurium,
                        int Terbium, int Prometid, int Duranium, int Xenomit, int Promerium, int Seprom)
        {
            this.Prometium = Prometium;
            this.Endurium = Endurium;
            this.Terbium = Terbium;
            this.Prometid = Prometid;
            this.Duranium = Duranium;
            this.Xenomit = Xenomit;
            this.Promerium = Promerium;
            this.Seprom = Seprom;
        }
    }
}
