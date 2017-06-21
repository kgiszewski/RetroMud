using RetroMud.Core.Collision.Detectors;
using RetroMud.Core.Maps;
using RetroMud.Core.Maps.Coordinates;

namespace RetroMud.Core.Rendering
{
    public class NonPlayingCharacterAnimator : IAnimateNonPlayingCharacters
    {
        public void Animate(IMap map)
        {
            //find all npc's
            //foreach
            //- apply movement strategy

            for (var row = 0; row < map.Buffer.Length; row++)
            {
                var rowCharacters = map.Buffer[row].ToCharArray();

                for (var column = 0; column < rowCharacters.Length; column++)
                {
                    //TODO: make a list of chars
                    if (rowCharacters[column] == '&')
                    {
                        var newPosition = _getProposedPosition(map, new MapCoordinate(row, column));
                        map.UpdateAtPosition(new MapCoordinate(row, column), ' ');
                        map.UpdateAtPosition(newPosition, '&');
                    }
                }
            }
        }

        //TODO: each bad guy will need it's own movement strategy
        private IMapCoordinate _getProposedPosition(IMap map, IMapCoordinate mapCoordinate)
        {
            var collisionDetector = new NonPlayingCharacterCollisionDetector();

            if (CollisionDetector.Instance.CanMoveToPosition(map, new MapCoordinate(mapCoordinate.Row, mapCoordinate.Column + 1)))
            {
                return new MapCoordinate(mapCoordinate.Row, mapCoordinate.Column + 1);
            }
            else if (CollisionDetector.Instance.CanMoveToPosition(map, new MapCoordinate(mapCoordinate.Row, mapCoordinate.Column - 1)))
            {
                return new MapCoordinate(mapCoordinate.Row, mapCoordinate.Column - 1);
            }

            return mapCoordinate;
        }
    }
}
