using RetroMud.Core.Maps.Coordinates;

namespace RetroMud.Core.Players
{
    public class Player : IPlayer
    {
        public int Id { get; set; }
        public IMapCoordinate Position { get; set; }
        public int Gold { get; set; }
        public void MoveTo(IMapCoordinate position)
        {
            Position.Row = position.Row;
            Position.Column = position.Column;
        }
    }
}
