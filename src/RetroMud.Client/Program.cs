using System;
using System.Threading;
using RetroMud.Messaging.Messages;
using RetroMud.Messaging.Publishing;

namespace RetroMud
{
    class Program
    {
        private static readonly Random Rand = new Random();

        static void Main(string[] args)
        {
            var clientId = Rand.Next(123123123);

            var tcpMessenger = new TcpMessenger();

            Thread.Sleep(1000);

            Console.WriteLine("My client Id: " + clientId);

            while (true)
            {
                //Console.ReadKey();

                Thread.Sleep(10);
                
                var response = tcpMessenger.Send(new FooMessage{Id = clientId});

                Console.WriteLine(response);
            }
        }
    }
}
