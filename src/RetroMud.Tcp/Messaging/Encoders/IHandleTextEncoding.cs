namespace RetroMud.Tcp.Messaging.Encoders
{
    public interface IHandleTextEncoding
    {
        byte[] GetBytes(string input);
        string GetString(byte[] input, int numberOfBytes);
    }
}
