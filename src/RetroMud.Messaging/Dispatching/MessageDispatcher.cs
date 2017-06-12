﻿using System;
using RetroMud.Messaging.Helpers;
using RetroMud.Messaging.Messages;

namespace RetroMud.Messaging.Dispatching
{
    public class MessageDispatcher : IDispatchMessages
    {
        public object Dispatch(ITcpMessage message)
        {
            Console.WriteLine($"Dispatching {message.MessageType}...");

            //in the case of 'FooMessage' this will return 'Foo'
            var nameOfMessageClass = message.GetType().Name.ToMessageTypeNameWithoutSuffix();

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