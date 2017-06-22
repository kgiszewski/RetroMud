using System;
using System.Linq;
using RetroMud.Core.Collision.Detectors;
using RetroMud.Core.Context;
using RetroMud.Core.Controls;
using RetroMud.Core.Maps;
using RetroMud.Core.Rendering;

namespace RetroMud.Core.Scenes
{
    public class ExploreMapScene : IGameScene
    {
        private readonly IMap _map;
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
            _collisionDetector = collisionDetector;
            _mapRenderer = mapRenderer;
            _mapMovementControls = mapMovementControls;
            _map = ClientContext.Instance.MapManager.GetById(mapId);
        }

        public void Setup()
        {
            Console.Clear();
        }

        public void Render()
        {                
            var statusMessageManager = ClientContext.Instance.StatusMessageManager;

            while (IsSceneActive)
            {
                var statusMessages = statusMessageManager.GetMessages(30);

                _mapMovementControls.HandleInput(_map);
                _collisionDetector.Update(_map, ClientContext.Instance.Player.Position);
                _mapRenderer.RenderMap(_map, statusMessages.Select(x => x.Message));
            }
        }
    }
}
