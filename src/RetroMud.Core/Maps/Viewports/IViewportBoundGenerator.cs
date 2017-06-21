namespace RetroMud.Core.Maps.Viewports
{
    public interface IViewportBoundGenerator
    {
        IViewportBounds GetBounds(IMap map, IMapViewport mapViewport, int currentRow, int currentColumn);
    }
}
