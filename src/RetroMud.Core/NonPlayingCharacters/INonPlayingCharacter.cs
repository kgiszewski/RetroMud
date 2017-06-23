using RetroMud.Core.Maps.Coordinates;
using RetroMud.Core.NonPlayingCharacters.Animation;

namespace RetroMud.Core.NonPlayingCharacters
{
    public interface INonPlayingCharacter
    {
        char Character { get; set; }
        IMapCoordinate Position { get; set; }
        IMovementStrategy MovementStrategy { get; set; }
    }
}
