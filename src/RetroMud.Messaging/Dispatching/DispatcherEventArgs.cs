using System;
using RetroMud.Messaging.Messages;

namespace RetroMud.Messaging.Dispatching
{
    public class DispatcherEventArgs : EventArgs
    {
        public ITcpMessage Message { get; set; }
        public Type HandlerType { get; set; }
    }
}
