using RetroMud.Messaging.Dispatching;
using RetroMud.Messaging.Helpers;

namespace RetroMud.Messaging.Messages
{
    public class FooMessage : ITcpMessage
    {
        public int Id { get; set; }
        public string MessageType => GetType().Name.ToMessageName();
    }
}
