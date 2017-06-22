using System;
using RetroMud.Core.Maps;
using RetroMud.Core.Maps.Coordinates;

namespace RetroMud.Core.Collision
{
    public class CollisionDetectedEventArgs : EventArgs
    {
        public char Character;
        public IMapCoordinate Position;
        public IMap Map;
    }
}
