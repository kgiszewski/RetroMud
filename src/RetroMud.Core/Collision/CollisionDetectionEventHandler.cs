using RetroMud.Core.Collision.Dispatching;
using RetroMud.Core.Context;
using RetroMud.Core.Events.Helpers;
using RetroMud.Core.Logging;
using RetroMud.Core.Maps.Wormholes;
using RetroMud.Core.Scenes;
using RetroMud.Core.Status;

namespace RetroMud.Core.Collision
{
    public class CollisionDetectionEventHandler : IRegisterClientEvents
    {
        private readonly IDispatchCollisions _collisionDispatcher;
        private readonly IWormholeManager _wormholeManager;
        private readonly IStatusMessageManager _statusMessageManager;

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
            Logger.Debug<CollisionDetectedHandler>($"The player has encountered a: {e.Character} character at position: {e.Column}, {e.Row}");

            _collisionDispatcher.Dispatch(e);
        }
    }
}
