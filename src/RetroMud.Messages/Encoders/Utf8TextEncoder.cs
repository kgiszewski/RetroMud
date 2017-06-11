using System.Text;

namespace RetroMud.Messaging.Encoders
{
    public class Utf8TextEncoder : IHandleTextEncoding
    {
        public byte[] GetBytes(string input)
        {
            return Encoding.UTF8.GetBytes(input);
        }

        public string GetString(byte[] input, int numberOfBytes)
        {
            return Encoding.UTF8.GetString(input, 0, numberOfBytes);
        }
    }
}
