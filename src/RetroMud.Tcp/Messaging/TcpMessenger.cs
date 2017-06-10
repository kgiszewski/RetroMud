using System.Net;
using System.Net.Sockets;
using System.Text;
using RetroMud.Tcp.Config;

namespace RetroMud.Tcp.Messaging
{
    public class TcpMessenger : ISendTcpMessages
    {
        private readonly ITcpConfiguration _tcpConfiguration;

        public TcpMessenger()
            : this(new TcpConfiguration())
        {
            
        }

        public TcpMessenger(ITcpConfiguration tcpConfiguration)
        {
            _tcpConfiguration = tcpConfiguration;
        }

        internal Socket GetSocket()
        {
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(new IPEndPoint(IPAddress.Parse(_tcpConfiguration.IpAddress), _tcpConfiguration.Port));

            return socket;
        }

        public string Send(string message)
        {
            var socket = GetSocket();

            socket.Send(Encoding.UTF8.GetBytes(message));

            var buffer = new byte[_tcpConfiguration.ReadBufferSizeInBytes];

            var numberBytes = socket.Receive(buffer);
            return Encoding.UTF8.GetString(buffer, 0, numberBytes);
        }
    }
}
