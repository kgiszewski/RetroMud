using System;
using log4net.Config;
using RetroMud.Core.Context;
using RetroMud.Core.Events.Helpers;
using RetroMud.Messaging.Publishing;
using RetroMud.Core.Healthchecks.Messages;
using RetroMud.Core.Players.Messages;
using RetroMud.Core.Scenes;
using RetroMud.Core.Status;

namespace RetroMud
{
    class Program
    {
        private static readonly Random Rand = new Random();
        private static ISendTcpMessages _messenger;

        static void Main(string[] args)
        {
            XmlConfigurator.Configure();

            Console.ReadKey();

            EventHelper.RegisterAllClientEventHandlers();

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

            ClientContext.Instance.Player = ((GetPlayerResponse)_messenger.Send(new GetPlayerRequest
            {
                PlayerId = 1
            })).Player;

            ClientContext.Instance.GameSceneManager = new GameSceneManager { CurrentGameScene = new ExploreMapScene(1)};

            ClientContext.Instance.StatusMessageManager = new StatusMessageManager();

            while (ClientContext.Instance.GameSceneManager.CurrentGameScene != null)
            {
                ClientContext.Instance.GameSceneManager.CurrentGameScene.Render();
            }

            Console.WriteLine("Game over!");

            Console.ReadKey();
        }
    }
}
