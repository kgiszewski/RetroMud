namespace RetroMud.Messaging.Messages.Healthchecks
{
    public class CurrentClientVersionResponse : ITcpResponseMessage
    {
        public bool RequiresUpgrade { get; set; }
    }
}
