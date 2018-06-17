using System.Collections.Generic;

namespace NettyBase.Game.world.objects.map.objects.stations
{
    class PirateStation : Station
    {
        public PirateStation(int id, Vector pos, Spacemap map) : base(id, new List<StationModule>(), Faction.NONE, pos, map)
        {

        }
    }
}