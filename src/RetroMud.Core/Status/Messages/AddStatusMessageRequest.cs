using RetroMud.Messaging.Messages;

namespace RetroMud.Core.Status.Messages
{
    public class AddStatusMessageRequest : TcpMessageBase
    {
        public int PlayerId { get; set; }
        public string Message { get; set; }
    }

    public class AddStatusMessageResponse : ITcpResponseMessage
    {
        
    }
}
