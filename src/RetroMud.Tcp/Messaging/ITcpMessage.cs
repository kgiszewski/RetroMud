namespace RetroMud.Tcp.Messaging
{
    public interface ITcpMessage
    {
        string MessageType { get; }
        int ClientId { get; }
    }
}
