namespace RetroMud.Core.Maps.Viewports
{
    public class ViewportBoundGenerator : IViewportBoundGenerator
    {
        public IViewportBounds GetBounds(IMap map, IMapViewport mapViewport, int currentRow, int currentColumn)
        {
            var leftLimit = currentColumn - mapViewport.ColumnSize;
            if (leftLimit < 0)
            {
                leftLimit = 0;
            }

            var rightLimit = currentColumn + mapViewport.ColumnSize;
            if (rightLimit > map.Width)
            {
                rightLimit = map.Width - mapViewport.ColumnSize;
            }

            var upperLimit = currentRow - mapViewport.RowSize;
            if (upperLimit < 0)
            {
                upperLimit = 0;
            }

            var lowerLimit = currentRow + mapViewport.RowSize;
            if (lowerLimit > map.Height)
            {
                lowerLimit = map.Height;
            }

            if (lowerLimit < mapViewport.RowSize * 2)
            {
                lowerLimit = mapViewport.RowSize * 2;
            }

            return new ViewportBounds(upperLimit, lowerLimit, leftLimit, rightLimit);
        }
    }
}
