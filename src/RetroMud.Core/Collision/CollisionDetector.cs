﻿using System.Collections.Generic;
using RetroMud.Core.Maps;

namespace RetroMud.Core.Collision
{
    public class CollisionDetector : IHandleCollisionDetection
    {
        private static readonly char _pathChar = ' ';
        private static IHandleCollisionDetection _collisionDetector;

        private static readonly List<char> _allowedToMoveToChars = new List<char>
        {
            _pathChar,
            '▒'
        };

        public event CollisionDetectedHandler OnCollision;

        private CollisionDetector()
        {

        }

        public static IHandleCollisionDetection Instance()
        {
            return _collisionDetector ?? (_collisionDetector = new CollisionDetector());
        }

        public bool CanMoveToPosition(IMap map, int row, int column)
        {
            var charAtPosition = _getCharAtPosition(map, row, column);

            return _allowedToMoveToChars.Contains(charAtPosition);
        }

        public void Update(IMap map, int row, int column)
        {
            var charAtPosition = _getCharAtPosition(map, row, column);

            if (charAtPosition != _pathChar)
            {
                OnCollision?.Invoke(this, new CollisionDetectedEventArgs
                {
                    Character = charAtPosition,
                    Column = column,
                    Row = row,
                    Map = map
                });
            }
        }

        private char _getCharAtPosition(IMap map, int row, int column)
        {
            var rowData = map.Data[row];

            return rowData[column];
        }
    }
}
