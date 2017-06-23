using RetroMud.Core.Maps.Coordinates;
using RetroMud.Core.NonPlayingCharacters.Animation;

namespace RetroMud.Core.NonPlayingCharacters
{
    public class NonPlayingCharacter : INonPlayingCharacter
    {
        public char Character { get; set; }
        public IMapCoordinate Position { get; set; }
        public IMovementStrategy MovementStrategy { get; set; }
    }
}
