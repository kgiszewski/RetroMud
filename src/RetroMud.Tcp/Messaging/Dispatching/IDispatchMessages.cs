namespace RetroMud.Tcp.Messaging.Dispatching
{
    public interface IDispatchMessages
    {
        object Dispatch(ITcpMessage message);
    }
}
