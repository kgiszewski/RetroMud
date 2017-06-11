using System;
using System.Linq;

namespace RetroMud.Core.Events.Helpers
{
    public static class EventHelper
    {
        public static void RegisterAllEventHandlers()
        {
            var eventHandlersTypes =
                System.Reflection.Assembly.GetExecutingAssembly()
                    .GetTypes()
                    .Where(mytype => mytype.GetInterfaces().Contains(typeof(IRegisterEvents)))
                    .ToList();

            foreach (var type in eventHandlersTypes)
            {
                var instance = Activator.CreateInstance(type);

                ((IRegisterEvents)instance).Register();
            }
        }
    }
}
