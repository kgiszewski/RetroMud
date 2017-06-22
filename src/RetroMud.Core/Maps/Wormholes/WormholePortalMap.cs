namespace RetroMud.Core.Maps.Wormholes
{
    public class WormholePortalMap : IWormholePortalMap
    {
        public IWormholePortal From { get; set; }
        public IWormholePortal To { get; set; }
    }
}
