using RetroMud.Messaging.Messages;

namespace RetroMud.Core.Players.Messages
{
    public class GetPlayerRequest : TcpMessageBase
    {
        public int PlayerId { get; set; }
    }

    public class GetPlayerResponse : ITcpResponseMessage
    {
        public Player Player { get; set; }
    }
}
