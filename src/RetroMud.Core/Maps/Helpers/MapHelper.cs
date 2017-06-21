using RetroMud.Core.Maps.Coordinates;

namespace RetroMud.Core.Maps.Helpers
{
    public static class MapHelper
    {
        public static char GetCharAtPosition(IMap map, IMapCoordinate position)
        {
            var rowData = map.Buffer[position.Row];

            return rowData[position.Column];
        }
    }
}
