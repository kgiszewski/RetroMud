using RetroMud.Messaging.Helpers;

namespace RetroMud.Messaging.Messages
{
    public abstract class TcpMessageBase : ITcpMessage
    {
        public string MessageType => GetType().Name.ToMessageTypeNameWithoutSuffix();
    }
}
