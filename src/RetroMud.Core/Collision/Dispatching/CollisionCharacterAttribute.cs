using System;

namespace RetroMud.Core.Collision.Dispatching
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CollisionCharacterAttribute : Attribute
    {
        public char CollisionCharacter { get; set; }

        public CollisionCharacterAttribute(char collisionCharacter)
        {
            CollisionCharacter = collisionCharacter;
        }
    }
}
