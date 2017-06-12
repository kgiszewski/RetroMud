using RetroMud.Messaging.Messages;

namespace RetroMud.Core.Healthchecks.Messages
{
    public class CurrentClientVersionRequest : TcpMessageBase
    {
        public int ClientId;
        public string CurrentVersion;
    }

    public class CurrentClientVersionResponse : ITcpResponseMessage
    {
        public bool RequiresUpgrade { get; set; }
    }
}
