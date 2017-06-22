using RetroMud.Core.Collision.Detectors;
using RetroMud.Core.Collision.Dispatching;
using RetroMud.Core.Events.Helpers;
using RetroMud.Core.Logging;

namespace RetroMud.Core.Collision
{
    public class CollisionDetectionEventHandler : IRegisterClientEvents
    {
        private readonly IDispatchCollisions _collisionDispatcher;

        public CollisionDetectionEventHandler()
            : this(new CollisionDispatcher())
        {
            
        }

        public CollisionDetectionEventHandler(IDispatchCollisions collisionDispatcher)
        {
            _collisionDispatcher = collisionDispatcher;
        }

        public void Register()
        {
            CollisionDetector.Instance.OnCollision += CollisionDetectionEventHandler_OnCollision;
        }

        private void CollisionDetectionEventHandler_OnCollision(object sending, CollisionDetectedEventArgs e)
        {
            Logger.Debug<CollisionDetectedHandler>($"The player has encountered a: {e.Character} character at position: {e.Position.Column}, {e.Position.Row}");

            _collisionDispatcher.Dispatch(e);
        }
    }
}
