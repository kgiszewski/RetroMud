using RetroMud.Core.Maps.Coordinates;

namespace RetroMud.Core.Maps.Wormholes
{
    public interface IWormholePortal
    {
        int MapId { get; set; }
        IMapCoordinate Position { get; set; }
    }
}
