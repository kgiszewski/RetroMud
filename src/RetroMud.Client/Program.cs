using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using RetroMud.Tcp.Config;
using RetroMud.Tcp.Messaging;

namespace RetroMud
{
    class Program
    {
        private static readonly Random _rand = new Random();

        static void Main(string[] args)
        {
            var clientId = _rand.Next(123123123);

            var tcpMessenger = new TcpMessenger();

            Console.WriteLine("My client Id: " + clientId);

            while (true)
            {
                Console.ReadKey();
                
                var response = tcpMessenger.Send("Hello world! " + clientId);

                Console.WriteLine(response);
            }
        }
    }
}
