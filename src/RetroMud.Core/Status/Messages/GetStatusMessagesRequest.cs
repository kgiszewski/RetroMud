using System.Collections.Generic;
using RetroMud.Messaging.Messages;

namespace RetroMud.Core.Status.Messages
{
    public class GetStatusMessagesRequest : TcpMessageBase
    {
        public int PlayerId { get; set; }
    }

    public class GetStatusMessagesResponse : ITcpResponseMessage
    {
        public IEnumerable<IStatusMessage> StatusMessages { get; set; }
    }
}
