namespace RetroMud.Tcp.Config
{
    public class TcpConfiguration : ITcpConfiguration
    {
        public string IpAddress => "127.0.0.1";
        public int Port => 30001;
        public int ReadBufferSizeInBytes => 8196;
    }
}
