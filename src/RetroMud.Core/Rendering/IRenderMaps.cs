using RetroMud.Core.Maps;
using RetroMud.Core.Players;

namespace RetroMud.Core.Rendering
{
    public interface IRenderMaps
    {
        void RenderMap(
            IMap map
        );
    }
}
