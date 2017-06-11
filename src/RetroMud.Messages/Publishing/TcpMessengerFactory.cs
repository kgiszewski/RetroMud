namespace RetroMud.Messaging.Publishing
{
    public static class TcpMessengerFactory
    {
        public static ISendTcpMessages GetMessenger()
        {
            return new TcpMessenger();
        }
    }
}
