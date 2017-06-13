namespace RetroMud.Core.Maps.Window
{
    public interface IWindowBoundGenerator
    {
        IWindowBounds GetBounds(IMap map, IMapWindow mapWindow, int currentRow, int currentColumn);
    }
}
