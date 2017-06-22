using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using RetroMud.Core.Config;
using RetroMud.Core.Maps.Wormholes;

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

            var jsonSettings = new JsonSerializerSettings();
            jsonSettings.Formatting = Formatting.Indented;

            var metaJson = JsonConvert.SerializeObject(new MapMetaData
            {
                Id = map.Id,
                WormholePortalMaps = _toPortalMap(map.WormholePortalMaps)
            }, jsonSettings);

            File.WriteAllLines(filePath, map.Buffer);
            File.AppendAllText(filePath, $"{ConfigConstants.MapMetaBoundary}\r\n{metaJson}");
        }

        private List<int[]> _toPortalMap(IEnumerable<IWormholePortalMap> portalMaps)
        {
            var list = new List<int[]>();

            foreach (var portalMap in portalMaps)
            {
                var portalMapping = new int[6];
                portalMapping[0] = portalMap.From.MapId;
                portalMapping[1] = portalMap.From.Position.Row;
                portalMapping[2] = portalMap.From.Position.Column;
                portalMapping[3] = portalMap.To.MapId;
                portalMapping[4] = portalMap.To.Position.Row;
                portalMapping[5] = portalMap.To.Position.Column;

                list.Add(portalMapping);
            }

            return list;
        }
    }
}
