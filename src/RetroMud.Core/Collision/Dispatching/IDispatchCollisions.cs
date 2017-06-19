namespace RetroMud.Core.Collision.Dispatching
{
    public interface IDispatchCollisions
    {
        void Dispatch(CollisionDetectedEventArgs eventArgs);
    }
}
