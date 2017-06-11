using System.Net;
using System.Net.Sockets;
using RetroMud.Messaging.Config;
using RetroMud.Messaging.Dispatching;
using RetroMud.Messaging.Encoders;
using RetroMud.Messaging.Serialization;

namespace RetroMud.Messaging.Publishing
{
    public class TcpMessenger : ISendTcpMessages
    {
        private readonly ITcpConfiguration _tcpConfiguration;
        private readonly IHandleTextEncoding _textEncoder;
        private readonly IHandleSerialization _serializer;

        public TcpMessenger()
            : this(
                  new TcpConfiguration(), 
                  new Utf8TextEncoder(),
                  new JsonSerializer()
                )
        {
            
        }

        public TcpMessenger(
            ITcpConfiguration tcpConfiguration, 
            IHandleTextEncoding textEncoder,
            IHandleSerialization serializer
        )
        {
            _tcpConfiguration = tcpConfiguration;
            _textEncoder = textEncoder;
            _serializer = serializer;
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

            socket.Send(_textEncoder.GetBytes(_serializer.Serialize(message)));

            var buffer = new byte[_tcpConfiguration.ReadBufferSizeInBytes];

            var numberBytes = socket.Receive(buffer);

            return _textEncoder.GetString(buffer, numberBytes);
        }
    }
}
