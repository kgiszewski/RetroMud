using System.Net.Sockets;

namespace RetroMud.Messaging.Server
{
    public static class TcpServerFactory
    {
        public static ITcpServer GetServer()
        {
            return new TcpServer();
        }

        public static ISocketHandler GetSocketHandler(Socket socket)
        {
            return new SocketHandler(socket);
        }
    }
}
