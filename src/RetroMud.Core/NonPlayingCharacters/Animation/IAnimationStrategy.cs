using RetroMud.Core.Maps;
using RetroMud.Core.Maps.Coordinates;

namespace RetroMud.Core.NonPlayingCharacters.Animation
{
    public interface IAnimationStrategy
    {
        IMapCoordinate GetNewPosition(IMap map, IMapCoordinate mapCoordinate, int frameNumber);
    }
}
