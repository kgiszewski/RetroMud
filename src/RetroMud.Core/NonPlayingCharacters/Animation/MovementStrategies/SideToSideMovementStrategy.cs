using RetroMud.Core.Collision.Detectors;
using RetroMud.Core.Maps;
using RetroMud.Core.Maps.Coordinates;
using RetroMud.Core.Rendering.Animation;

namespace RetroMud.Core.NonPlayingCharacters.Animation.MovementStrategies
{
    public class SideToSideMovementStrategy : IAnimationStrategy
    {
        private readonly IHandleCollisionDetection _collisionDetector;
        private bool _isMovingLeft;

        public SideToSideMovementStrategy()
            :this(new NonPlayingCharacterCollisionDetector())
        {
            
        }

        public SideToSideMovementStrategy(IHandleCollisionDetection collisionDetector)
        {
            _collisionDetector = collisionDetector;
        }

        public IMapCoordinate GetNewPosition(IMap map, IMapCoordinate mapCoordinate)
        {
            if (_isMovingLeft)
            {
                if (_collisionDetector.CanMoveToPosition(map,
                    new MapCoordinate(mapCoordinate.Row, mapCoordinate.Column - 1)))
                {
                    return new MapCoordinate(mapCoordinate.Row, mapCoordinate.Column - 1);
                }
                else
                {
                    _isMovingLeft = false;
                }
            }
            else
            {
                if (_collisionDetector.CanMoveToPosition(map,
                    new MapCoordinate(mapCoordinate.Row, mapCoordinate.Column + 1)))
                {
                    return new MapCoordinate(mapCoordinate.Row, mapCoordinate.Column + 1);
                }
                else
                {
                    _isMovingLeft = true;
                }
            }

            return mapCoordinate;
        }
    }
}
