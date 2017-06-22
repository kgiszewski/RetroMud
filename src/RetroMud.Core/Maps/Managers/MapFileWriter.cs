using System.IO;
using Newtonsoft.Json;
using RetroMud.Core.Config;

namespace RetroMud.Core.Maps.Managers
{
    public class MapFileWriter : IMapFileWriter
    {
        public void Write(IMap map, string filePath)
        {
            var directory = Path.GetDirectoryName(filePath);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var metaJson = JsonConvert.SerializeObject(new MapMetaData
            {
                Id = map.Id
            }, Formatting.Indented);

            File.WriteAllLines(filePath, map.Buffer);
            File.AppendAllText(filePath, $"{ConfigConstants.MapMetaBoundary}\r\n{metaJson}");
        }
    }
}
