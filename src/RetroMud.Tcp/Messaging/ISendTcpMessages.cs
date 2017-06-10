namespace RetroMud.Tcp.Messaging
{
    public interface ISendTcpMessages
    {
        string Send(ITcpMessage message);
    }
}
