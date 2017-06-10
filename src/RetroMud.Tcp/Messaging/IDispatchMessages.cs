using System.Net.Sockets;

namespace RetroMud.Tcp.Messaging
{
    public interface IDispatchMessages
    {
        void Dispatch(Socket socket);
    }
}
