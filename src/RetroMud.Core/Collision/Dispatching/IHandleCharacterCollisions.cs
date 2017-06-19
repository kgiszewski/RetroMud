namespace RetroMud.Core.Collision.Dispatching
{
    public interface IHandleCharacterCollisions
    {
        void Handle(CollisionDetectedEventArgs eventArgs);
    }
}
