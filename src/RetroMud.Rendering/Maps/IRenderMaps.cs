using RetroMud.Core.Collision;
using RetroMud.Core.Maps;
using RetroMud.Core.Maps.Window;
using RetroMud.Core.Players;

namespace RetroMud.Rendering.Maps
{
    public interface IRenderMaps
    {
        void RenderMap(
            IMap map,
            IPlayer player
        );
    }
}
