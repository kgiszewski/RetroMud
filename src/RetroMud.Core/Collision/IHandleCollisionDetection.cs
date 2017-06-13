using RetroMud.Core.Maps;
using RetroMud.Core.Players;

namespace RetroMud.Core.Collision
{
    public interface IHandleCollisionDetection
    {
        bool CanMovePosition(IMap map, int row, int column);
    }
}
