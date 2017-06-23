using System.Collections.Generic;
using System.Linq;
using RetroMud.Core.Maps.Coordinates;
using RetroMud.Core.NonPlayingCharacters;

namespace RetroMud.Core.Maps.Helpers
{
    public static class MapHelper
    {
        public static char GetCharAtPosition(IMap map, IMapCoordinate position)
        {
            var rowData = map.Buffer[position.Row];

            return rowData[position.Column];
        }

        public static List<INonPlayingCharacter> GetNpcForMap(IMap map)
        {
            var npcList = new List<INonPlayingCharacter>();
            var allPossibleNpcCharacters = _getAllPossibleNpcCharacters().ToList();

            for (var row = 0; row < map.Buffer.Length; row++)
            {
                var rowCharacters = map.Buffer[row].ToCharArray();

                for (var column = 0; column < rowCharacters.Length; column++)
                {
                    if (allPossibleNpcCharacters.Any(x => x == rowCharacters[column]))
                    {
                        npcList.Add(NonPlayingCharacterFactory.Create(rowCharacters[column], new MapCoordinate(row, column)));
                    }
                }
            }

            return npcList;
        }

        private static IEnumerable<char> _getAllPossibleNpcCharacters()
        {
            return new List<char> {'&', '*'};
        }
    }
}
