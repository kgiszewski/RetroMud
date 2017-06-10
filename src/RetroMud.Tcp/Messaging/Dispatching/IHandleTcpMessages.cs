namespace RetroMud.Tcp.Messaging.Dispatching
{
    public interface IHandleTcpMessages<in TMessage> where TMessage : ITcpMessage
    {
        object Handle(TMessage message);
    }
}
