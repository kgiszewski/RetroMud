namespace RetroMud.Core.Maps.Coordinates
{
    public class MapMapCoordinate : IMapCoordinate
    {
        public MapMapCoordinate(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public int Row { get; set; }
        public int Column { get; set; }
    }
}
