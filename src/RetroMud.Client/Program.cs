using System;
using RetroMud.Messaging.Messages.Healthchecks;
using RetroMud.Messaging.Publishing;

namespace RetroMud
{
    class Program
    {
        private static readonly Random Rand = new Random();
        private static ISendTcpMessages _messenger;

        static void Main(string[] args)
        {
            var clientId = Rand.Next(123123123);
            var clientVersion = "0.1.0";

            _messenger = TcpMessengerFactory.GetMessenger();

            Console.WriteLine("Sending healthcheck info...");

            var response = ((CurrentClientVersionResponse)_messenger.Send(new CurrentClientVersion
            {
                ClientId = clientId,
                CurrentVersion = clientVersion
            }));

            Console.WriteLine($"Requires upgrade: {response.RequiresUpgrade}");

            Console.ReadKey();
        }
    }
}
