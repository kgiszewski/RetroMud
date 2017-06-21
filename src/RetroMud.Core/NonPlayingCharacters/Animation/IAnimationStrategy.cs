using RetroMud.Core.Maps;
using RetroMud.Core.Maps.Coordinates;

namespace RetroMud.Core.Rendering.Animation
{
    public interface IAnimationStrategy
    {
        IMapCoordinate GetNewPosition(IMap map, IMapCoordinate mapCoordinate);
    }
}
