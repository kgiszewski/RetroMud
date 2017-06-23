using RetroMud.Core.Maps.Coordinates;
using RetroMud.Core.NonPlayingCharacters.Animation;
using RetroMud.Core.NonPlayingCharacters.Animation.MovementStrategies;

namespace RetroMud.Core.NonPlayingCharacters
{
    public static class NonPlayingCharacterFactory
    {
        public static INonPlayingCharacter Create(char character, IMapCoordinate position)
        {
            var movementStrategy = _getMovementStrategy(character);

            return new NonPlayingCharacter
            {
                Position = new MapCoordinate(position),
                MovementStrategy = movementStrategy,
                Character = character
            };
        }

        private static IMovementStrategy _getMovementStrategy(char character)
        {
            switch (character)
            {
                case '*':
                    return new HomingMovementStrategy(10);

                case '#':
                    return new UpAndDownMovementStrategy(10);

                default:
                    return new SideToSideMovementStrategy(7);
            }
        }
    }
}
