using System;
using System.Linq;

namespace RetroMud.Core.Events.Helpers
{
    public static class EventHelper
    {
        public static void RegisterAllServerEventHandlers()
        {
            var eventHandlersTypes =
                System.Reflection.Assembly.GetExecutingAssembly()
                    .GetTypes()
                    .Where(mytype => mytype.GetInterfaces().Contains(typeof(IRegisterServerEvents)))
                    .ToList();

            foreach (var type in eventHandlersTypes)
            {
                var instance = Activator.CreateInstance(type);

                ((IRegisterServerEvents)instance).Register();
            }
        }

        public static void RegisterAllClientEventHandlers()
        {
            var eventHandlersTypes =
                System.Reflection.Assembly.GetExecutingAssembly()
                    .GetTypes()
                    .Where(mytype => mytype.GetInterfaces().Contains(typeof(IRegisterClientEvents)))
                    .ToList();

            foreach (var type in eventHandlersTypes)
            {
                var instance = Activator.CreateInstance(type);

                ((IRegisterClientEvents)instance).Register();
            }
        }
    }
}
