using System.Net.Sockets;
using RetroMud.Tcp.Config;

namespace RetroMud.Tcp.Messaging
{
    public interface ISendTcpMessages
    {
        string Send(string message);
    }
}
