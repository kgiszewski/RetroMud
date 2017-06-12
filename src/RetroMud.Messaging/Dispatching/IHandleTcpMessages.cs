using RetroMud.Messaging.Messages;

namespace RetroMud.Messaging.Dispatching
{
    public interface IHandleTcpMessages<in TMessage, out TResponse> 
        where TMessage : ITcpMessage
        where TResponse : ITcpResponseMessage
    {
        TResponse Handle(TMessage message);
    }
}
