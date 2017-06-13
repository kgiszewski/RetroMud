using System;

namespace RetroMud.Core.Maps.CharacterColors
{
    public class CharacterColor : ICharacterColor
    {
        public char Character { get; set; }
        public ConsoleColor Color { get; set; }

        public CharacterColor(char character, ConsoleColor color)
        {
            Character = character;
            Color = color;
        }
    }
}
