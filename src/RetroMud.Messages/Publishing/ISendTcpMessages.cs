using RetroMud.Messaging.Dispatching;

namespace RetroMud.Messaging.Publishing
{
    public interface ISendTcpMessages
    {
        string Send(ITcpMessage message);
    }
}
