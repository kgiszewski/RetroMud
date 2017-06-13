using System;

namespace RetroMud.Core.Maps.CharacterColors
{
    public interface ICharacterColor
    {
        char Character { get; set; }
        ConsoleColor Color { get; set; }
    }
}
