using RetroMud.Messaging.Messages;

namespace RetroMud.Messaging.Dispatching
{
    public interface IDispatchMessages
    {
        object Dispatch(ITcpMessage message);
    }
}
