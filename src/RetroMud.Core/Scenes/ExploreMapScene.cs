using System;
using RetroMud.Core.Collision;
using RetroMud.Core.Context;
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

        public bool IsSceneActive { get; set; }

        public ExploreMapScene(int mapId)
            :this (mapId, CollisionDetector.Instance, new MapRenderer())
        {
            
        }

        public ExploreMapScene(
            int mapId, 
            IHandleCollisionDetection collisionDetector,
            IRenderMaps mapRenderer
        )
        {
            IsSceneActive = true;
            _mapId = mapId;
            _collisionDetector = collisionDetector;
            _mapRenderer = mapRenderer;
        }

        public void Render()
        {
            _messenger = TcpMessengerFactory.GetMessenger();
            
            var rawResponse = _messenger.Send(new GetMapRequest
            {
                MapId = _mapId
            });

            var getMapResponse = (GetMapResponse)rawResponse;

            var map = getMapResponse.Map;

            _mapRenderer.RenderMap(map);

            var player = GameContext.Instance.Player;

            while (IsSceneActive)
            {
                if (Console.KeyAvailable)
                {
                    var input = Console.ReadKey(true);

                    if (input.KeyChar == 'a' && player.CurrentColumn > 0 &&
                        _collisionDetector.CanMoveToPosition(map, player.CurrentRow, player.CurrentColumn - 1))
                    {
                        player.CurrentColumn--;
                    }

                    if (input.KeyChar == 'w' && player.CurrentRow > 0 &&
                        _collisionDetector.CanMoveToPosition(map, player.CurrentRow - 1, player.CurrentColumn))
                    {
                        player.CurrentRow--;
                    }

                    if (input.KeyChar == 'd' && player.CurrentColumn < map.Width - 1 &&
                        _collisionDetector.CanMoveToPosition(map, player.CurrentRow, player.CurrentColumn + 1))
                    {
                        player.CurrentColumn++;
                    }

                    if (input.KeyChar == 's' && player.CurrentRow < map.Height - 1 &&
                        _collisionDetector.CanMoveToPosition(map, player.CurrentRow + 1, player.CurrentColumn))
                    {
                        player.CurrentRow++;
                    }
                }

                _collisionDetector.Update(map, player.CurrentRow, player.CurrentColumn);
                _mapRenderer.RenderMap(map);
            }
        }
    }
}
