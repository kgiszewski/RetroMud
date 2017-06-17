﻿using System.Linq;
using RetroMud.Core.Collision;
using RetroMud.Core.Context;
using RetroMud.Core.Controls;
using RetroMud.Core.Maps.Messages;
using RetroMud.Core.Rendering;
using RetroMud.Messaging.Publishing;

namespace RetroMud.Core.Scenes
{
    public class ExploreMapScene : IGameScene
    {
        private static ISendTcpMessages _messenger;
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
            _messenger = TcpMessengerFactory.GetMessenger();
            
            var getMapResponse = _messenger.Send<GetMapResponse>(new GetMapRequest
            {
                MapId = _mapId
            });

            var map = getMapResponse.Map;

            var statusMessageManager = ClientContext.Instance.StatusMessageManager;

            var statusMessages = statusMessageManager.GetMessages(10);

            _mapRenderer.RenderMap(map, statusMessages.Select(x => x.Message));

            var player = ClientContext.Instance.Player;

            while (IsSceneActive)
            {
                statusMessages = statusMessageManager.GetMessages(10);

                _mapMovementControls.HandleInput(map, player);
                _collisionDetector.Update(map, player.CurrentRow, player.CurrentColumn);
                _mapRenderer.RenderMap(map, statusMessages.Select(x => x.Message));
            }
        }
    }
}
