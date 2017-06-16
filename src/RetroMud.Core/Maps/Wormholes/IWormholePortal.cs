namespace RetroMud.Core.Maps.Wormholes
{
    public interface IWormholePortal
    {
        int MapId { get; set; }
        int Row { get; set; }
        int Column { get; set; }
    }
}
