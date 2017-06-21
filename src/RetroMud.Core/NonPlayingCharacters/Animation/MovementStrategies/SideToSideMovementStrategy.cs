using System;
using RetroMud.Core.Collision.Detectors;
using RetroMud.Core.Config;
using RetroMud.Core.Maps;
using RetroMud.Core.Maps.Coordinates;

namespace RetroMud.Core.NonPlayingCharacters.Animation.MovementStrategies
{
    public class SideToSideMovementStrategy : IAnimationStrategy
    {
        private readonly IHandleCollisionDetection _collisionDetector;
        private bool _isMovingLeft;
        private int _updateFrequency;

        public SideToSideMovementStrategy(int updateFrequency)
            :this(updateFrequency, new NonPlayingCharacterCollisionDetector())
        {
            
        }

        public SideToSideMovementStrategy(int updateFrequency, IHandleCollisionDetection collisionDetector)
        {
            if (updateFrequency > ConfigConstants.MaxGameFrameRate || updateFrequency < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(_updateFrequency));
            }

            _updateFrequency = updateFrequency;
            _collisionDetector = collisionDetector;
        }

        public IMapCoordinate GetNewPosition(IMap map, IMapCoordinate mapCoordinate, int frameNumber)
        {
            if (frameNumber % _updateFrequency == 0)
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

            return mapCoordinate;
        }
    }
}
