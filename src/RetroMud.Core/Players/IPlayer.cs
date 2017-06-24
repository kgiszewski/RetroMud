using RetroMud.Core.Config;
using RetroMud.Core.Maps.Coordinates;

namespace RetroMud.Core.Players
{
    public interface IPlayer
    {
        int Id { get; set; }
        IMapCoordinate Position { get; set; }
        int Gold { get; set; }
        void MoveTo(IMapCoordinate position);
        bool IsAttacking { get; set; }
        Direction Facing { get; set; }
        int BeginAttackFrame { get; set; }
    }
}
