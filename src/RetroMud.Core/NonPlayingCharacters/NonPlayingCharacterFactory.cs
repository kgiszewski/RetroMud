using RetroMud.Core.Maps.Coordinates;
using RetroMud.Core.NonPlayingCharacters.Animation.MovementStrategies;

namespace RetroMud.Core.NonPlayingCharacters
{
    public static class NonPlayingCharacterFactory
    {
        public static INonPlayingCharacter Create(char character, IMapCoordinate position)
        {
            var movementStrategy = new SideToSideMovementStrategy(7);

            return new NonPlayingCharacter
            {
                Position = new MapCoordinate(position.Row, position.Column),
                AnimationStrategy = movementStrategy,
                Character = character
            };
        }
    }
}
