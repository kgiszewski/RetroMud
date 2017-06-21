using RetroMud.Core.Maps.Coordinates;
using RetroMud.Core.Rendering.Animation;

namespace RetroMud.Core.NonPlayingCharacters
{
    public interface INonPlayingCharacter
    {
        char Character { get; set; }
        IMapCoordinate Position { get; set; }
        IAnimationStrategy AnimationStrategy { get; set; }
    }
}
