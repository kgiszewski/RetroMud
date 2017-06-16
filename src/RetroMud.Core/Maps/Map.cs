using System;
using System.Collections.Generic;
using RetroMud.Core.Maps.CharacterColors;

namespace RetroMud.Core.Maps
{
    public class Map : IMap
    {
        public int Id { get; set; }
        public int Width => Data[0].Length;
        public int Height => Data.Length;
        public string[] Data { get; set; }
        public List<CharacterColor> CharacterColors { get; set; }

        public Map(int mapId)
        {
            Id = mapId;

            if (mapId == 1)
            {
                Data = System.IO.File.ReadAllLines(@"..\..\..\RetroMud.Core\Maps\Data\HelloWorld.txt");
                CharacterColors = new List<CharacterColor>
                {
                    new CharacterColor('@', ConsoleColor.Cyan),
                    new CharacterColor('▒', ConsoleColor.Red),
                };
            }

            if (mapId == 2)
            {
                Data = System.IO.File.ReadAllLines(@"..\..\..\RetroMud.Core\Maps\Data\AnotherWorld.txt");
                CharacterColors = new List<CharacterColor>
                {
                    new CharacterColor('@', ConsoleColor.Cyan),
                    new CharacterColor('▒', ConsoleColor.Green),
                };
            }
        }
    }
}
