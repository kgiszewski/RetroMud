using System.Collections.Generic;
using System.Linq;
using RetroMud.Core.Maps.CharacterColors;
using RetroMud.Core.Maps.Wormholes;

namespace RetroMud.Core.Maps
{
    public static class MapFactory
    {
        public static IMap Create(int mapId, IEnumerable<ICharacterColor> characterColors, string[] buffer, IEnumerable<IWormholePortalMap> wormholePortalMaps)
        {
            return new Map
            {
                Id = mapId,
                CharacterColors = characterColors.ToList(),
                Buffer = buffer,
                WormholePortalMaps = wormholePortalMaps
            };
        }
    }
}
