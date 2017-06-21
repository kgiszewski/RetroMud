using RetroMud.Core.Maps.Coordinates;
using RetroMud.Core.Rendering.Animation;

namespace RetroMud.Core.NonPlayingCharacters
{
    public class NonPlayingCharacter : INonPlayingCharacter
    {
        public char Character { get; set; }
        public IMapCoordinate Position { get; set; }
        public IAnimationStrategy AnimationStrategy { get; set; }
    }
}
