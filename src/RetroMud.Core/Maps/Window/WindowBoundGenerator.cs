namespace RetroMud.Core.Maps.Window
{
    public class WindowBoundGenerator : IWindowBoundGenerator
    {
        public IWindowBounds GetBounds(IMap map, IMapWindow mapWindow, int currentRow, int currentColumn)
        {
            var leftLimit = currentColumn - mapWindow.ColumnSize;
            if (leftLimit < 0)
            {
                leftLimit = 0;
            }

            var rightLimit = currentColumn + mapWindow.ColumnSize;
            if (rightLimit > map.Width)
            {
                rightLimit = map.Width - mapWindow.ColumnSize;
            }

            var upperLimit = currentRow - mapWindow.RowSize;
            if (upperLimit < 0)
            {
                upperLimit = 0;
            }

            var lowerLimit = currentRow + mapWindow.RowSize;
            if (lowerLimit > map.Height)
            {
                lowerLimit = map.Height;
            }

            if (lowerLimit < mapWindow.RowSize * 2)
            {
                lowerLimit = mapWindow.RowSize * 2;
            }

            return new WindowBounds(upperLimit, lowerLimit, leftLimit, rightLimit);
        }
    }
}
