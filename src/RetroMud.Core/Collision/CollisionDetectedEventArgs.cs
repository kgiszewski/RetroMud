using System;
using RetroMud.Core.Maps;

namespace RetroMud.Core.Collision
{
    public class CollisionDetectedEventArgs : EventArgs
    {
        public char Character;
        public int Row;
        public int Column;
        public IMap Map;
    }
}
