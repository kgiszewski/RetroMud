using System;
using System.Net.Sockets;
using Newtonsoft.Json;
using RetroMud.Tcp.Config;
using RetroMud.Tcp.Messaging.Encoders;
using RetroMud.Tcp.Messaging.Helpers;

namespace RetroMud.Tcp.Messaging.Dispatching
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

        public object Dispatch(ITcpMessage message)
        {
            Console.WriteLine("Dispatching...");

            //in the case of 'FooMessage' this will return 'Foo'
            var nameOfMessageClass = message.GetType().Name.ToMessageName();

            //this goes and gets the handler type based on the name of the message class
            var handlerType = MessageHelper.GetMessageHandlerByMessageTypeName(nameOfMessageClass);

            //this creates an instance at runtime
            var handlerInstance = Activator.CreateInstance(handlerType);

            //finally we execute the new handler instance
            //i'm not a fan of the 'dynamic' keyword but it works well here
            return ((dynamic)handlerInstance).Handle((dynamic)message);
        }
    }
}
