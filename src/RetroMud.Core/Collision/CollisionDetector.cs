using RetroMud.Core.Maps;

namespace RetroMud.Core.Collision
{
    public class CollisionDetector : IHandleCollisionDetection
    {
        public bool CanMovePosition(IMap map, int row, int column)
        {
            var charAtPosition = _getCharAtPosition(map, row, column);

            return charAtPosition == ' ';
        }

        private char _getCharAtPosition(IMap map, int row, int column)
        {
            var rowData = map.Data[row];

            return rowData[column];
        }
    }
}
