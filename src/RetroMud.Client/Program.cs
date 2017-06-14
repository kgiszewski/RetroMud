using System;
using RetroMud.Messaging.Publishing;
using RetroMud.Core.Healthchecks.Messages;
using RetroMud.Core.Players;
using RetroMud.Core.Players.Messages;
using RetroMud.Rendering.Scenes;

namespace RetroMud
{
    class Program
    {
        private static readonly Random Rand = new Random();
        private static ISendTcpMessages _messenger;

        static void Main(string[] args)
        {
            Console.ReadKey();

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

            var player = ((GetPlayerResponse)_messenger.Send(new GetPlayerRequest
            {
                PlayerId = 1
            })).Player;

            var scene = new ExploreMapScene(1, player);

            scene.Render();
        }
    }
}
