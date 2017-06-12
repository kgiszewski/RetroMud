using System;
using System.Net.Sockets;
using System.Threading;
using RetroMud.Messaging.Config;
using RetroMud.Messaging.Dispatching;
using RetroMud.Messaging.Encoders;
using RetroMud.Messaging.Helpers;
using RetroMud.Messaging.Messages;
using RetroMud.Messaging.Serialization;

namespace RetroMud.Messaging.Server
{
    public class SocketHandler : ISocketHandler
    {
        private readonly Socket _socket;
        private Thread _workerThread;
        public bool Completed { get; set; }
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
                 new JsonSerializer()
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

        public void StartWorker()
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

                var serializedResponse = _serializer.Serialize(response);

                _socket.Send(_textEncoder.GetBytes(serializedResponse));
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
