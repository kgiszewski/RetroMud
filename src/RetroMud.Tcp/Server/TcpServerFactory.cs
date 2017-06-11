using System.Net.Sockets;

namespace RetroMud.Tcp.Server
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
