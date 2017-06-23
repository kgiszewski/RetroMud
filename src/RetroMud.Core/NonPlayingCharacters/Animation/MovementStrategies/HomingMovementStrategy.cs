using System;
using RetroMud.Core.Collision.Detectors;
using RetroMud.Core.Config;
using RetroMud.Core.Context;
using RetroMud.Core.Maps;
using RetroMud.Core.Maps.Coordinates;

namespace RetroMud.Core.NonPlayingCharacters.Animation.MovementStrategies
{
    public class HomingMovementStrategy : IMovementStrategy
    {
        private readonly IHandleCollisionDetection _collisionDetector;
        private readonly int _movementRate;

        public HomingMovementStrategy(int updateFrequency)
            : this(updateFrequency, new NonPlayingCharacterCollisionDetector())
        {
            
        }

        public HomingMovementStrategy(int updateFrequency, IHandleCollisionDetection collisionDetector)
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
            var playerPosition = ClientContext.Instance.Player.Position;

            if (frameNumber % _movementRate == 0)
            {
                //if is not on same row, try to move to the same row
                if (!_isOnSameRow(mapCoordinate, playerPosition))
                {
                    if (_shouldMoveUp(mapCoordinate, playerPosition))
                    {
                        var proposedLocation = new MapCoordinate(mapCoordinate.Row - 1, mapCoordinate.Column);

                        if (_collisionDetector.CanMoveToPosition(map, proposedLocation))
                        {
                            return proposedLocation;
                        }
                    }
                    else
                    {
                        //should move down
                        var proposedLocation = new MapCoordinate(mapCoordinate.Row + 1, mapCoordinate.Column);

                        if (_collisionDetector.CanMoveToPosition(map, proposedLocation))
                        {
                            return proposedLocation;
                        }
                    }
                }
                else
                {
                    //on same row, so move columns
                    var sideToSideMovement = _handleSideToSideMovement(map, mapCoordinate, playerPosition);

                    if (sideToSideMovement != null)
                    {
                        return sideToSideMovement;
                    }
                }

                //avoid getting stuck
                var sideToSideMovement2 = _handleSideToSideMovement(map, mapCoordinate, playerPosition);

                if (sideToSideMovement2 != null)
                {
                    return sideToSideMovement2;
                }
            }

            return mapCoordinate;
        }

        private IMapCoordinate _handleSideToSideMovement(IMap map, IMapCoordinate mapCoordinate, IMapCoordinate playerPosition)
        {
            if (_shouldMoveLeft(mapCoordinate, playerPosition))
            {
                var proposedLocation = new MapCoordinate(mapCoordinate.Row, mapCoordinate.Column - 1);

                if (_collisionDetector.CanMoveToPosition(map, proposedLocation))
                {
                    return proposedLocation;
                }
            }
            else
            {
                var proposedLocation = new MapCoordinate(mapCoordinate.Row, mapCoordinate.Column + 1);

                //move right
                if (_collisionDetector.CanMoveToPosition(map, proposedLocation))
                {
                    return proposedLocation;
                }
            }

            return null;
        }

        private bool _isOnSameRow(IMapCoordinate npcMapCoordinate, IMapCoordinate playerMapCoordinate)
        {
            return npcMapCoordinate.Row == playerMapCoordinate.Row;
        }

        private bool _shouldMoveUp(IMapCoordinate npcMapCoordinate, IMapCoordinate playerMapCoordinate)
        {
            return npcMapCoordinate.Row > playerMapCoordinate.Row;
        }

        private bool _shouldMoveLeft(IMapCoordinate npcMapCoordinate, IMapCoordinate playerMapCoordinate)
        {
            return npcMapCoordinate.Column > playerMapCoordinate.Column;
        }
    }
}
