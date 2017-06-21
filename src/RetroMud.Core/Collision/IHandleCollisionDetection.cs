using RetroMud.Core.Maps;
using RetroMud.Core.Maps.Coordinates;

namespace RetroMud.Core.Collision
{
    public delegate void CollisionDetectedHandler(object sending, CollisionDetectedEventArgs e);

    public interface IHandleCollisionDetection
    {
        event CollisionDetectedHandler OnCollision;
        bool CanMoveToPosition(IMap map, IMapCoordinate position);
        void Update(IMap map, IMapCoordinate position);
    }
}
