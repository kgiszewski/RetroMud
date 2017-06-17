using RetroMud.Messaging.Messages;

namespace RetroMud.Messaging.Publishing
{
    public interface ISendTcpMessages
    {
        TResponse Send<TResponse>(ITcpMessage message);
    }
}
