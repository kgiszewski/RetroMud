namespace RetroMud.Core.Maps.Wormholes
{
    public interface IWormholeManager
    {
        IWormholePortal RouteFrom(IWormholePortal portal);
    }
}
