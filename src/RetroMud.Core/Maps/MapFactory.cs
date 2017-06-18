using System.Runtime.Remoting.Messaging;

namespace RetroMud.Core.Maps
{
    public static class MapFactory
    {
        public static IMap Get(int mapId)
        {
            return new Map(mapId);
        }
    }
}
