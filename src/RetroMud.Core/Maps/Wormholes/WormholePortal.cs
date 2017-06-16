namespace RetroMud.Core.Maps.Wormholes
{
    public class WormholePortal : IWormholePortal
    {
        public int MapId { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
    }
}
