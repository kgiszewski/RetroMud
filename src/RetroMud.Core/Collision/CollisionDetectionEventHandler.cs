using RetroMud.Core.Context;
using RetroMud.Core.Events.Helpers;
using RetroMud.Core.Logging;
using RetroMud.Core.Maps.Wormholes;
using RetroMud.Core.Scenes;

namespace RetroMud.Core.Collision
{
    public class CollisionDetectionEventHandler : IRegisterClientEvents
    {
        private readonly IWormholeManager _wormholeManager;

        public CollisionDetectionEventHandler()
            : this(new WormholeManager())
        {
            
        }

        public CollisionDetectionEventHandler(IWormholeManager wormholeManager)
        {
            _wormholeManager = wormholeManager;
        }

        public void Register()
        {
            CollisionDetector.Instance.OnCollision += CollisionDetectionEventHandler_OnCollision;
        }

        private void CollisionDetectionEventHandler_OnCollision(object sending, CollisionDetectedEventArgs e)
        {
            Logger.Debug<CollisionDetectedHandler>($"The player has encountered a: {e.Character} character at position: {e.Column}, {e.Row}");

            if (e.Character == '▒')
            {
                var destinationPortal = _wormholeManager.RouteFrom(new WormholePortal
                {
                    MapId = e.Map.Id,
                    Row = e.Row,
                    Column = e.Column
                });

                if (destinationPortal != null)
                {
                    GameContext.Instance.GameSceneManager.CurrentGameScene.IsSceneActive = false;
                    GameContext.Instance.GameSceneManager.CurrentGameScene = new ExploreMapScene(destinationPortal.MapId);
                    GameContext.Instance.Player.CurrentColumn = destinationPortal.Column;
                    GameContext.Instance.Player.CurrentRow = destinationPortal.Row;
                }
            }
        }
    }
}
