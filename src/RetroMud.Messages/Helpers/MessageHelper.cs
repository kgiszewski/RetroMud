using System;
using System.Collections.Generic;
using System.Linq;
using RetroMud.Messaging.Dispatching;
using RetroMud.Messaging.Messages;

namespace RetroMud.Messaging.Helpers
{
    public static class MessageHelper
    {
        private static IEnumerable<Type> _allHandlerTypes;
        private static IEnumerable<Type> _allMessageTypes;
        private static string _handlerSuffix = "Handler";
        private static string _messageSuffix = "Message";
        private static string _responseSuffix = "Response";

        //based on message name, fine the corresponding handler type
        public static Type GetMessageHandlerByMessageTypeName(string messageType)
        {
            var handler = GetAllMessageHandlers()
                .FirstOrDefault(x => x.Name.ToHandlerTypeNameWithoutSuffix() == messageType);

            return handler;
        }

        //reflection utility to cache the known handler types
        public static IEnumerable<Type> GetAllMessageHandlers()
        {
            if (_allHandlerTypes != null) return _allHandlerTypes;

            var openGenericType = typeof(IHandleTcpMessages<,>);

            _allHandlerTypes = from x in AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                               from z in x.GetInterfaces()
                               let y = x.BaseType
                               where
                               ((y != null && y.IsGenericType && openGenericType.IsAssignableFrom(y.GetGenericTypeDefinition()))
                                || (z.IsGenericType && openGenericType.IsAssignableFrom(z.GetGenericTypeDefinition())))
                               && !x.IsAbstract && !x.IsInterface
                               select x;

            return _allHandlerTypes;
        }

        //given an input, returns the raw message name when given a handler name
        public static string ToHandlerTypeNameWithoutSuffix(this string input)
        {
            if (input == null)
            {
                return string.Empty;
            }

            return !input.EndsWith(_handlerSuffix) ? input : input.Remove(input.LastIndexOf(_handlerSuffix));
        }

        //given an input, returns the raw message name when given a message name
        public static string ToMessageTypeNameWithoutSuffix(this string input)
        {
            if (input == null)
            {
                return string.Empty;
            }

            return !input.EndsWith(_messageSuffix) ? input : input.Remove(input.LastIndexOf(_messageSuffix));
        }

        public static Type GetMessageTypeByName(string messageName)
        {
            if (_allMessageTypes == null)
            {
                var messageType = typeof(ITcpMessage);

                _allMessageTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                    .Where(
                        x => messageType.IsAssignableFrom(x)
                             && !x.IsAbstract
                    ).ToList();
            }

            var pocoType = _allMessageTypes.FirstOrDefault(x => x.Name.ToMessageTypeNameWithoutSuffix().ToLower() == messageName.ToMessageTypeNameWithoutSuffix().ToLower());

            return pocoType;
        }

        public static Type GetMessageResponseTypeByName(string messageName)
        {
            messageName += _responseSuffix;

            if (_allMessageTypes == null)
            {
                var messageType = typeof(ITcpResponseMessage);

                _allMessageTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                    .Where(
                        x => messageType.IsAssignableFrom(x)
                             && !x.IsAbstract
                    ).ToList();
            }

            var pocoType = _allMessageTypes.FirstOrDefault(x => x.Name.ToMessageTypeNameWithoutSuffix().ToLower() == messageName.ToMessageTypeNameWithoutSuffix().ToLower());

            return pocoType;
        }
    }
}
