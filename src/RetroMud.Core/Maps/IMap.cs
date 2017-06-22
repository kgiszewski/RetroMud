using System.Collections.Generic;
using RetroMud.Core.Maps.CharacterColors;
using RetroMud.Core.Maps.Coordinates;
using RetroMud.Core.Maps.Wormholes;

namespace RetroMud.Core.Maps
{
    public interface IMap
    {
        int Id { get; }
        int Width { get; }
        int Height { get; }
        string[] Buffer { get; set; }
        List<ICharacterColor> CharacterColors { get; set; }
        void UpdateBufferAtPosition(IMapCoordinate position, char character);
        IEnumerable<IWormholePortalMap> WormholePortalMaps { get; set; }
    }
}
