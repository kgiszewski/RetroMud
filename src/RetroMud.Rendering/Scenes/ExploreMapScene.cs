using System;
using RetroMud.Core.Maps.Messages;
using RetroMud.Core.Maps.Window;
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

        public ExploreMapScene(int mapId, IPlayer player)
        {
            _mapId = mapId;
            _player = player;
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

            var mapWindow = new MapWindow();
            var boundGenerator = new WindowBoundGenerator();

            var mapRenderer = new MapRenderer();

            mapRenderer.RenderMap(map, mapWindow, boundGenerator, _player);

            while (true)
            {
                var input = Console.ReadKey(true);

                if (input.KeyChar == 'a' && _player.CurrentColumn > 0)
                {
                    _player.CurrentColumn--;
                }

                if (input.KeyChar == 'w' && _player.CurrentRow > 0)
                {
                    _player.CurrentRow--;
                }

                if (input.KeyChar == 'd' && _player.CurrentColumn < map.Width - 1)
                {
                    _player.CurrentColumn++;
                }

                if (input.KeyChar == 's' && _player.CurrentRow < map.Height - 1)
                {
                    _player.CurrentRow++;
                }

                mapRenderer.RenderMap(map, mapWindow, boundGenerator, _player);
            }
        }
    }
}
