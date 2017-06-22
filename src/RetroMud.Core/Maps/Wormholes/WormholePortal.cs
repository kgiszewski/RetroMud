using RetroMud.Core.Maps.Coordinates;

namespace RetroMud.Core.Maps.Wormholes
{
    public class WormholePortal : IWormholePortal
    {
        public int MapId { get; set; }
        public IMapCoordinate Position { get; set; }
    }
}
