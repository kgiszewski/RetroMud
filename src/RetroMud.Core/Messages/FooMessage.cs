using RetroMud.Tcp.Messaging.Dispatching;
using RetroMud.Tcp.Messaging.Helpers;

namespace RetroMud.Core.Messages
{
    public class FooMessage : ITcpMessage
    {
        public int Id { get; set; }
        public string MessageType => GetType().Name.ToMessageName();
    }
}
