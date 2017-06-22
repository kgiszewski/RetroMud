using System.Collections.Generic;

namespace RetroMud.Core.Maps.Managers
{
    public class MapMetaData : IMapMetaData
    {
        public int Id { get; set; }
        public List<int[]> WormholePortalMaps { get; set; }
    }
}
