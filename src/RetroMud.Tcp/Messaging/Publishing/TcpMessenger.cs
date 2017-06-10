using System.Net;
using System.Net.Sockets;
using Newtonsoft.Json;
using RetroMud.Tcp.Config;
using RetroMud.Tcp.Messaging.Dispatching;
using RetroMud.Tcp.Messaging.Encoders;

namespace RetroMud.Tcp.Messaging.Publishing
{
    public class TcpMessenger : ISendTcpMessages
    {
        private readonly ITcpConfiguration _tcpConfiguration;
        private readonly IHandleTextEncoding _textEncoder;

        public TcpMessenger()
            : this(new TcpConfiguration(), new Utf8TextEncoder())
        {
            
        }

        public TcpMessenger(ITcpConfiguration tcpConfiguration, IHandleTextEncoding textEncoder)
        {
            _tcpConfiguration = tcpConfiguration;
            _textEncoder = textEncoder;
        }

        internal Socket GetSocket()
        {
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(new IPEndPoint(IPAddress.Parse(_tcpConfiguration.IpAddress), _tcpConfiguration.Port));

            return socket;
        }

        public string Send(ITcpMessage message)
        {
            var socket = GetSocket();

            socket.Send(_textEncoder.GetBytes(JsonConvert.SerializeObject(message)));

            var buffer = new byte[_tcpConfiguration.ReadBufferSizeInBytes];

            var numberBytes = socket.Receive(buffer);

            return _textEncoder.GetString(buffer, numberBytes);
        }
    }
}
