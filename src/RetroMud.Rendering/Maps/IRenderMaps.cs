using RetroMud.Core.Maps;
using RetroMud.Core.Maps.Window;

namespace RetroMud.Rendering.Maps
{
    public interface IRenderMaps
    {
        void RenderMap(IMap map, IMapWindow mapWindow, IWindowBoundGenerator boundGenerator, int currentColumn, int currentRow);
    }
}
