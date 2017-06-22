using System.Collections.Generic;

namespace RetroMud.Core.Maps.Managers
{
    public interface IMapMetaData
    {
        int Id { get; set; }
        List<int[]> WormholePortalMaps { get; set; }
    }
}
