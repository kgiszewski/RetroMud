using RetroMud.Messaging.Dispatching;

namespace RetroMud.Messaging.Messages.Healthchecks
{
    public class CurrentClientVersion : TcpMessageBase
    {
        public int ClientId;
        public string CurrentVersion;
    }
}
