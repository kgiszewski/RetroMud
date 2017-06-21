using System.Collections.Generic;
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

            for (var row = 0; row < map.Buffer.Length; row++)
            {
                var rowCharacters = map.Buffer[row].ToCharArray();

                for (var column = 0; column < rowCharacters.Length; column++)
                {
                    //TODO: make a list of chars
                    if (rowCharacters[column] == '&')
                    {
                        npcList.Add(NonPlayingCharacterFactory.Create('&', new MapCoordinate(row, column)));
                    }
                }
            }

            return npcList;
        }
    }
}
