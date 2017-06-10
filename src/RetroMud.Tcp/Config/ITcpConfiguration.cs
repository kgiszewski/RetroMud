namespace RetroMud.Tcp.Config
{
    public interface ITcpConfiguration
    {
        string IpAddress { get; }
        int Port { get; }
        int ReadBufferSizeInBytes { get; }
    }
}
