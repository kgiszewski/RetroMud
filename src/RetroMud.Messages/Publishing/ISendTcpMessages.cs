using RetroMud.Messaging.Messages;

namespace RetroMud.Messaging.Publishing
{
    public interface ISendTcpMessages
    {
        object Send(ITcpMessage message);
    }
}
