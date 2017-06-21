using RetroMud.Core.Maps.Coordinates;
using RetroMud.Core.NonPlayingCharacters.Animation.MovementStrategies;

namespace RetroMud.Core.NonPlayingCharacters
{
    public static class NonPlayingCharacterFactory
    {
        public static INonPlayingCharacter Create(char character, IMapCoordinate position)
        {
            var movementStrategy = new SideToSideMovementStrategy();

            return new NonPlayingCharacter
            {
                Position = position,
                AnimationStrategy = movementStrategy,
                Character = character
            };
        }
    }
}
