using System.Linq;
using RetroMud.Core.Collision;
using RetroMud.Core.Context;
using RetroMud.Core.Controls;
using RetroMud.Core.Maps;
using RetroMud.Core.Rendering;

namespace RetroMud.Core.Scenes
{
    public class ExploreMapScene : IGameScene
    {
        private readonly int _mapId;
        private readonly IHandleCollisionDetection _collisionDetector;
        private readonly IRenderMaps _mapRenderer;
        private readonly IHandleMapMovementControls _mapMovementControls;

        public bool IsSceneActive { get; set; }

        public ExploreMapScene(int mapId)
            :this (mapId, CollisionDetector.Instance, new MapRenderer(), new KeyboardMapMovementController())
        {
            
        }

        public ExploreMapScene(
            int mapId, 
            IHandleCollisionDetection collisionDetector,
            IRenderMaps mapRenderer,
            IHandleMapMovementControls mapMovementControls
        )
        {
            IsSceneActive = true;
            _mapId = mapId;
            _collisionDetector = collisionDetector;
            _mapRenderer = mapRenderer;
            _mapMovementControls = mapMovementControls;
        }

        public void Render()
        {
            var map = MapFactory.Get(_mapId);
                
            var statusMessageManager = ClientContext.Instance.StatusMessageManager;

            var statusMessages = statusMessageManager.GetMessages(30);

            _mapRenderer.RenderMap(map, statusMessages.Select(x => x.Message));

            var player = ClientContext.Instance.Player;

            while (IsSceneActive)
            {
                statusMessages = statusMessageManager.GetMessages(30);

                _mapMovementControls.HandleInput(map);
                _collisionDetector.Update(map, player.CurrentRow, player.CurrentColumn);
                _mapRenderer.RenderMap(map, statusMessages.Select(x => x.Message));
            }
        }
    }
}
