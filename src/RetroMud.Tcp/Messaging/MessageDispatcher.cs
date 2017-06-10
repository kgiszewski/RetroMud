using System;
using System.Net.Sockets;
using Newtonsoft.Json;
using RetroMud.Tcp.Config;

namespace RetroMud.Tcp.Messaging
{
    public class MessageDispatcher : IDispatchMessages
    {
        private readonly ITcpConfiguration _tcpConfiguration;
        private readonly IHandleTextEncoding _textEncoder;

        public MessageDispatcher()
            :this(new TcpConfiguration(), new Utf8TextEncoder())
        {
            
        }

        public MessageDispatcher(ITcpConfiguration tcpConfiguration, IHandleTextEncoding textEncoder)
        {
            _tcpConfiguration = tcpConfiguration;
            _textEncoder = textEncoder;
        }

        public void Dispatch(Socket socket)
        {
            Console.WriteLine("Dispatching...");
            var buffer = new byte[_tcpConfiguration.ReadBufferSizeInBytes];
            var numberBytes = socket.Receive(buffer);

            var rawMessage = _textEncoder.GetString(buffer, numberBytes);

            dynamic message = JsonConvert.DeserializeObject(rawMessage);

            Console.WriteLine(message.MessageType + " " + message.ClientId);

            //instantiate a handler
            //return the response

            socket.Send(_textEncoder.GetBytes("My response to you!" + message.MessageType));
        }
    }
}
