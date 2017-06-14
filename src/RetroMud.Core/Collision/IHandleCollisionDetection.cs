using RetroMud.Core.Maps;

namespace RetroMud.Core.Collision
{
    public delegate void CollisionDetectedHandler(object sending, CollisionDetectedEventArgs e);

    public interface IHandleCollisionDetection
    {
        event CollisionDetectedHandler OnCollision;
        bool CanMoveToPosition(IMap map, int row, int column);
        void Update(IMap map, int row, int column);
    }
}
