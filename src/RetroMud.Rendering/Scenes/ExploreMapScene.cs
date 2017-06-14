using System;
using RetroMud.Core.Collision;
using RetroMud.Core.Maps.Messages;
using RetroMud.Core.Players;
using RetroMud.Messaging.Publishing;
using RetroMud.Rendering.Maps;

namespace RetroMud.Rendering.Scenes
{
    public class ExploreMapScene : IGameScene
    {
        private static ISendTcpMessages _messenger;
        private readonly int _mapId;
        private readonly IPlayer _player;
        private readonly IHandleCollisionDetection _collisionDetector;
        private readonly IRenderMaps _mapRenderer;

        public ExploreMapScene(int mapId, IPlayer player)
            :this (mapId, player, CollisionDetector.Instance(), new MapRenderer())
        {
            
        }

        public ExploreMapScene(
            int mapId, 
            IPlayer player,
            IHandleCollisionDetection collisionDetector,
            IRenderMaps mapRenderer
        )
        {
            _mapId = mapId;
            _player = player;
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

            _mapRenderer.RenderMap(map, _player);

            while (true)
            {
                var input = Console.ReadKey(true);

                if (input.KeyChar == 'a' && _player.CurrentColumn > 0 && _collisionDetector.CanMoveToPosition(map, _player.CurrentRow, _player.CurrentColumn - 1))
                {
                    _player.CurrentColumn--;
                }

                if (input.KeyChar == 'w' && _player.CurrentRow > 0 && _collisionDetector.CanMoveToPosition(map, _player.CurrentRow - 1, _player.CurrentColumn))
                {
                    _player.CurrentRow--;
                }

                if (input.KeyChar == 'd' && _player.CurrentColumn < map.Width - 1 && _collisionDetector.CanMoveToPosition(map, _player.CurrentRow, _player.CurrentColumn + 1))
                {
                    _player.CurrentColumn++;
                }

                if (input.KeyChar == 's' && _player.CurrentRow < map.Height - 1 && _collisionDetector.CanMoveToPosition(map, _player.CurrentRow + 1, _player.CurrentColumn))
                {
                    _player.CurrentRow++;
                }

                _collisionDetector.Update(map, _player.CurrentRow, _player.CurrentColumn);
                _mapRenderer.RenderMap(map, _player);
            }
        }
    }
}
