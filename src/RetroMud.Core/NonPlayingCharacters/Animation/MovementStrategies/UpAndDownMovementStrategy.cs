using System;
using RetroMud.Core.Collision.Detectors;
using RetroMud.Core.Config;
using RetroMud.Core.Maps;
using RetroMud.Core.Maps.Coordinates;

namespace RetroMud.Core.NonPlayingCharacters.Animation.MovementStrategies
{
    public class UpAndDownMovementStrategy : IMovementStrategy
    {
        private readonly IHandleCollisionDetection _collisionDetector;
        private bool _isMovingUp;
        private readonly int _movementRate;

        public UpAndDownMovementStrategy(int updateFrequency)
            :this(updateFrequency, new NonPlayingCharacterCollisionDetector())
        {
            
        }

        public UpAndDownMovementStrategy(int updateFrequency, IHandleCollisionDetection collisionDetector)
        {
            if (updateFrequency > ConfigConstants.MaxGameFrameRate || updateFrequency < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(_movementRate));
            }

            _movementRate = updateFrequency;
            _collisionDetector = collisionDetector;
        }

        public IMapCoordinate GetNewPosition(IMap map, IMapCoordinate mapCoordinate, int frameNumber)
        {
            if (frameNumber % _movementRate == 0)
            {
                if (_isMovingUp)
                {
                    if (_collisionDetector.CanMoveToPosition(map,
                        new MapCoordinate(mapCoordinate.Row - 1, mapCoordinate.Column)))
                    {
                        return new MapCoordinate(mapCoordinate.Row - 1, mapCoordinate.Column);
                    }
                    else
                    {
                        _isMovingUp = false;
                    }
                }
                else
                {
                    if (_collisionDetector.CanMoveToPosition(map,
                        new MapCoordinate(mapCoordinate.Row + 1, mapCoordinate.Column)))
                    {
                        return new MapCoordinate(mapCoordinate.Row + 1, mapCoordinate.Column);
                    }
                    else
                    {
                        _isMovingUp = true;
                    }
                }

                return mapCoordinate;
            }

            return mapCoordinate;
        }
    }
}
