using System;
using System.Collections.Generic;
using RetroMud.Core.Maps;
using RetroMud.Core.Maps.Coordinates;
using RetroMud.Core.Maps.Helpers;

namespace RetroMud.Core.Collision.Detectors
{
    public class NonPlayingCharacterCollisionDetector : IHandleCollisionDetection
    {
        private static readonly char _pathChar = ' ';
   
        private static readonly List<char> AllowedToMoveToChars = new List<char>
        {
            _pathChar,
            '@'
        };

        public event CollisionDetectedHandler OnCollision;
        public bool CanMoveToPosition(IMap map, IMapCoordinate position)
        {
            var charAtPosition = MapHelper.GetCharAtPosition(map, position);

            return AllowedToMoveToChars.Contains(charAtPosition);
        }

        public void Update(IMap map, IMapCoordinate position)
        {
            throw new NotImplementedException();
        }
    }
}
