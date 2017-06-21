using System.Collections.Generic;
using RetroMud.Core.Maps;
using RetroMud.Core.Maps.Coordinates;

namespace RetroMud.Core.Collision
{
    public class CollisionDetector : IHandleCollisionDetection
    {
        private static readonly char _pathChar = ' ';
        private static IHandleCollisionDetection _collisionDetector;

        private static readonly List<char> AllowedToMoveToChars = new List<char>
        {
            _pathChar,
            '▒',
            '$'
        };

        public event CollisionDetectedHandler OnCollision;

        private CollisionDetector()
        {

        }

        public static IHandleCollisionDetection Instance => _collisionDetector ?? (_collisionDetector = new CollisionDetector());

        public bool CanMoveToPosition(IMap map, IMapCoordinate position)
        {
            var charAtPosition = _getCharAtPosition(map, position);

            return AllowedToMoveToChars.Contains(charAtPosition);
        }

        public void Update(IMap map, IMapCoordinate position)
        {
            var charAtPosition = _getCharAtPosition(map, position);

            if (charAtPosition != _pathChar)
            {
                OnCollision?.Invoke(this, new CollisionDetectedEventArgs
                {
                    Character = charAtPosition,
                    Column = position.Column,
                    Row = position.Row,
                    Map = map
                });
            }
        }

        private char _getCharAtPosition(IMap map, IMapCoordinate position)
        {
            var rowData = map.Buffer[position.Row];

            return rowData[position.Column];
        }
    }
}
