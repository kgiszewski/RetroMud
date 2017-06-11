using System;
using System.Net.Sockets;
using System.Threading;
using RetroMud.Tcp.Config;
using RetroMud.Tcp.Messaging.Dispatching;
using RetroMud.Tcp.Messaging.Encoders;
using RetroMud.Tcp.Messaging.Helpers;
using RetroMud.Tcp.Serialization;

namespace RetroMud.Tcp.Server
{
    public class SocketHandler
    {
        private readonly Socket _socket;
        private Thread _workerThread;
        public bool Completed;
        private readonly IDispatchMessages _messageDispatcher;
        private readonly ITcpConfiguration _tcpConfiguration;
        private readonly IHandleTextEncoding _textEncoder;
        private readonly IHandleSerialization _serializer;

        public SocketHandler(Socket socket)
            :this(
                 socket, 
                 new MessageDispatcher(), 
                 new TcpConfiguration(), 
                 new Utf8TextEncoder(),
                 new Serialization.JsonSerializer()
            )
        {
            
        }

        public SocketHandler(
            Socket socket, 
            IDispatchMessages messageDispatcher, 
            ITcpConfiguration tcpConfiguration, 
            IHandleTextEncoding textEncoder,
            IHandleSerialization serializer
        )
        {
            _socket = socket;
            _messageDispatcher = messageDispatcher;
            _tcpConfiguration = tcpConfiguration;
            _textEncoder = textEncoder;
            _serializer = serializer;
        }

        public void StartSocketWorker()
        {
            if (_socket == null) return;

            _workerThread = new Thread(_executeWork);

            _workerThread.Start();
        }

        //find correct handler and execute
        private void _executeWork()
        {
            try
            {
                var buffer = new byte[_tcpConfiguration.ReadBufferSizeInBytes];
                var numberBytes = _socket.Receive(buffer);

                var rawMessage = _textEncoder.GetString(buffer, numberBytes);

                var messageTypeName = ((dynamic)_serializer.Deserialize(rawMessage)).MessageType.ToString();

                var messageType = MessageHelper.GetMessageTypeByName(messageTypeName);

                var deserialized = _serializer.Deserialize(rawMessage, messageType) ;

                var response = _messageDispatcher.Dispatch((ITcpMessage)deserialized);

                _socket.Send(_textEncoder.GetBytes(_serializer.Serialize(response)));
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                Completed = true;
            }
        }

        public void Cleanup()
        {
            if (_workerThread.IsAlive)
            {
                _workerThread.Abort();
            }

            _socket.Close();
        }
    }
}
