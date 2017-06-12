using System;
using RetroMud.Messaging.Publishing;
using RetroMud.Rendering.Maps;
using RetroMud.Core.Healthchecks.Messages;
using RetroMud.Core.Maps;
using RetroMud.Core.Maps.Messages;

namespace RetroMud
{
    class Program
    {
        private static readonly Random Rand = new Random();
        private static ISendTcpMessages _messenger;

        static void Main(string[] args)
        {
            Console.WindowWidth = 150;
            Console.WindowHeight = 40;

            var clientId = Rand.Next(123123123);
            var clientVersion = "0.1.0";

            _messenger = TcpMessengerFactory.GetMessenger();

            Console.WriteLine("Sending healthcheck info...");

            var response = ((CurrentClientVersionResponse)_messenger.Send(new CurrentClientVersionRequest
            {
                ClientId = clientId,
                CurrentVersion = clientVersion
            }));

            Console.WriteLine($"Requires upgrade: {response.RequiresUpgrade}");

            IMap map = null;
            
            var rawResponse = _messenger.Send(new GetMapRequest
            {
                MapId = 1
            });

            var getMapResponse = (GetMapResponse)rawResponse;

            map = getMapResponse.Map;

            var mapWindow = new MapWindow();

            var currentColumn = 54;
            var currentRow = 7;

            var mapRenderer = new MapRenderer();
            
            mapRenderer.RenderMap(map.Data, mapWindow.RowSize, mapWindow.ColumnSize, currentColumn, currentRow);

            while (true)
            {
                var input = Console.ReadKey(true);

                if (input.KeyChar == 'a' && currentColumn > 0)
                {
                    currentColumn--;
                }

                if (input.KeyChar == 'w' && currentRow > 0)
                {
                    currentRow--;
                }

                if (input.KeyChar == 'd' && currentColumn < map.Width - 1)
                {
                    currentColumn++;
                }

                if (input.KeyChar == 's' && currentRow < map.Height - 1)
                {
                    currentRow++;
                }

                mapRenderer.RenderMap(map.Data, mapWindow.RowSize, mapWindow.ColumnSize, currentColumn, currentRow);
            }
        }
    }
}
