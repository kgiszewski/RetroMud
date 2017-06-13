using System.Collections.Generic;
using RetroMud.Core.Maps.CharacterColors;

namespace RetroMud.Core.Maps
{
    public interface IMap
    {
        int Width { get; }
        int Height { get; }
        string[] Data { get; set; }
        List<CharacterColor> CharacterColors { get; set; }
    }
}
