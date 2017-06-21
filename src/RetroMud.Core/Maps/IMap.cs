using System.Collections.Generic;
using RetroMud.Core.Maps.CharacterColors;
using RetroMud.Core.Maps.Coordinates;

namespace RetroMud.Core.Maps
{
    public interface IMap
    {
        int Id { get; }
        int Width { get; }
        int Height { get; }
        string[] Buffer { get; set; }
        List<CharacterColor> CharacterColors { get; set; }
        void UpdateAtPosition(IMapCoordinate position, char character);
        void SaveAsAltered();
    }
}
