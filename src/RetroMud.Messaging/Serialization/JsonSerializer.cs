using System;
using Newtonsoft.Json;

namespace RetroMud.Messaging.Serialization
{
    public class JsonSerializer : IHandleSerialization
    {
        public string Serialize(object input)
        {
            return JsonConvert.SerializeObject(input);
        }

        public object Deserialize(string input)
        {
            return JsonConvert.DeserializeObject(input);
        }

        public object Deserialize(string input, Type type)
        {
            return JsonConvert.DeserializeObject(input, type);
        }

        public T Deserialize<T>(string input)
        {
            return JsonConvert.DeserializeObject<T>(input);
        }
    }
}
