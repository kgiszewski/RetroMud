using System.Collections.Generic;
using RetroMud.Core.Maps.CharacterColors;

namespace RetroMud.Core.Maps
{
    public interface IMap
    {
        int Id { get; }
        int Width { get; }
        int Height { get; }
        string[] Buffer { get; set; }
        List<CharacterColor> CharacterColors { get; set; }
        void UpdateAtPosition(int row, int column, char character);
        void SaveAsAltered();
    }
}
