using System.Collections.Generic;
using System.Linq;
using RetroMud.Core.Maps.CharacterColors;

namespace RetroMud.Core.Maps
{
    public static class MapFactory
    {
        public static IMap Create(int mapId, IEnumerable<ICharacterColor> characterColors, string[] buffer)
        {
            return new Map
            {
                Id = mapId,
                CharacterColors = characterColors.ToList(),
                Buffer = buffer
            };
        }
    }
}
