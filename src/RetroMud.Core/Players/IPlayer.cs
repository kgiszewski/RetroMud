using RetroMud.Core.Maps.Coordinates;

namespace RetroMud.Core.Players
{
    public interface IPlayer
    {
        int Id { get; set; }
        IMapCoordinate Position { get; set; }
        int Gold { get; set; }
    }
}
