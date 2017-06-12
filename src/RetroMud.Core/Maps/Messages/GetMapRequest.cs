using RetroMud.Messaging.Messages;

namespace RetroMud.Core.Maps.Messages
{
    public class GetMapRequest : TcpMessageBase
    {
        public int MapId { get; set; }
    }

    public class GetMapResponse : ITcpResponseMessage
    {
        public Map Map { get; set; }
    }
}
