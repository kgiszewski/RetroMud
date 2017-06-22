namespace RetroMud.Core.Maps.Wormholes
{
    public interface IWormholePortalMap
    {
        IWormholePortal From { get; set; }
        IWormholePortal To { get; set; }
    }
}
