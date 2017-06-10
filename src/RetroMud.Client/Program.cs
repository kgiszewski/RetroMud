using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace RetroMud
{
    class Program
    {
        private static Random _rand = new Random();

        static void Main(string[] args)
        {
            var clientId = _rand.Next(123123123);

            while (true)
            {
                Console.ReadKey();

                var clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                clientSocket.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 30001));

                clientSocket.Send(Encoding.ASCII.GetBytes("Hello world! " + clientId));

                var buffer = new byte[128];

                var length = clientSocket.Receive(buffer);

                Console.WriteLine(Encoding.ASCII.GetString(buffer));
            }
        }
    }
}
