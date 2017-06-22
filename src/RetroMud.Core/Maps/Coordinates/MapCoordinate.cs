using Newtonsoft.Json;

namespace RetroMud.Core.Maps.Coordinates
{
    public class MapCoordinate : IMapCoordinate
    {
        [JsonConstructor]
        public MapCoordinate(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public MapCoordinate(IMapCoordinate mapCoordinate)
        {
            Row = mapCoordinate.Row;
            Column = mapCoordinate.Column;
        }

        public int Row { get; set; }
        public int Column { get; set; }
    }
}
