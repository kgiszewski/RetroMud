using System;
using Newtonsoft.Json;

namespace RetroMud.Messaging.Serialization
{
    public class JsonSerializer : IHandleSerialization
    {
        public string Serialize(object input)
        {
            return JsonConvert.SerializeObject(input, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });
        }

        public object Deserialize(string input)
        {
            return JsonConvert.DeserializeObject(input, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });
        }

        public object Deserialize(string input, Type type)
        {
            return JsonConvert.DeserializeObject(input, type, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });
        }

        public T Deserialize<T>(string input)
        {
            return JsonConvert.DeserializeObject<T>(input, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });
        }
    }
}
