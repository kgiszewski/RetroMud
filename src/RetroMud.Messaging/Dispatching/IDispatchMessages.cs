using RetroMud.Messaging.Messages;

namespace RetroMud.Messaging.Dispatching
{
    public delegate void DispatchEventHandler(object sender, DispatcherEventArgs e);

    public interface IDispatchMessages
    {
        event DispatchEventHandler OnDispatchMessage;
        object Dispatch(ITcpMessage message);
    }
}
