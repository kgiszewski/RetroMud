using RetroMud.Tcp.Messaging.Dispatching;

namespace RetroMud.Tcp.Messaging.Publishing
{
    public interface ISendTcpMessages
    {
        string Send(ITcpMessage message);
    }
}
