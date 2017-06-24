using System;
using log4net.Config;
using RetroMud.Core.Context;
using RetroMud.Core.Events.Helpers;
using RetroMud.Core.GameTicks;
using RetroMud.Core.Healthchecks.Messages;
using RetroMud.Core.Maps.Managers;
using RetroMud.Core.Maps.Wormholes;
using RetroMud.Core.Players.Messages;
using RetroMud.Core.Scenes;
using RetroMud.Core.Status;
using RetroMud.Messaging.Publishing;

namespace RetroMud
{
    public static class Setup
    {
        private static readonly Random Rand = new Random();
        private static ISendTcpMessages _messenger;

        public static void Initialize()
        {
            XmlConfigurator.Configure();

            EventHelper.RegisterAllClientEventHandlers();

            Console.WindowWidth = 150;
            Console.WindowHeight = 40;

            var clientId = Rand.Next(123123123);
            var clientVersion = "0.1.0";

            _messenger = TcpMessengerFactory.GetMessenger();

            Console.WriteLine("Sending healthcheck info...");

            var healthCheckResponse = _messenger.Send<CurrentClientVersionResponse>(new CurrentClientVersionRequest
            {
                ClientId = clientId,
                CurrentVersion = clientVersion
            });

            Console.WriteLine($"Requires upgrade: {healthCheckResponse.RequiresUpgrade}");
            Console.Clear();

            ClientContext.Instance.Player = _messenger.Send<GetPlayerResponse>(new GetPlayerRequest
            {
                PlayerId = 1
            }).Player;

            ClientContext.Instance.GameSceneManager = new GameSceneManager();
            ClientContext.Instance.GameSceneManager.ChangeToNextScene(new StartSplashScene());

            ClientContext.Instance.StatusMessageManager = new StatusMessageManager();
            ClientContext.Instance.MapManager = new FileSystemMapManager();
            ClientContext.Instance.WormholeManager = new WormholeManager();
            ClientContext.Instance.GameTickManager = new GameTickManager();
        }
    }
}
