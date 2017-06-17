using System.Collections.Generic;
using RetroMud.Messaging.Messages;

namespace RetroMud.Core.Status.Messages
{
    public class GetStatusMessagesRequest : TcpMessageBase
    {
    }

    public class GetStatusMessagesResponse : ITcpResponseMessage
    {
        public List<StatusMessage> StatusMessages { get; set; }
    }
}
