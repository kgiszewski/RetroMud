using RetroMud.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RetroMud.Core.Collision.Dispatching
{
    public class CollisionDispatcher : IDispatchCollisions
    {
        private static Dictionary<char, Type> _collisionHandlerTypes;

        public void Dispatch(CollisionDetectedEventArgs eventArgs)
        {
            var handlerType = GetCollisionHandler(eventArgs.Character);

            if (handlerType != null)
            {
                var instance = Activator.CreateInstance(handlerType);

                ((IHandleCharacterCollisions)instance).Handle(eventArgs);
            }
            else
            {
                Logger.Error<CollisionDispatcher>($"There was no matching handler for character {eventArgs.Character}!");
            }
        }

        private Type GetCollisionHandler(char character)
        {
            if (_collisionHandlerTypes == null)
            {
                _collisionHandlerTypes = new Dictionary<char, Type>();

               var allCollisionHandlers = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                    .Where(
                        x => x.GetCustomAttributes(typeof(CollisionCharacterAttribute), true).Length > 0
                    ).ToList();

                foreach (var handler in allCollisionHandlers)
                {
                    var attribute = handler.GetCustomAttribute(typeof(CollisionCharacterAttribute)) as CollisionCharacterAttribute;

                    if (attribute != null)
                    {
                        _collisionHandlerTypes.Add(attribute.CollisionCharacter, handler);
                    }
                }
            }

            if (_collisionHandlerTypes.ContainsKey(character))
            {
                return _collisionHandlerTypes[character];
            }

            return null;
        }
    }
}
