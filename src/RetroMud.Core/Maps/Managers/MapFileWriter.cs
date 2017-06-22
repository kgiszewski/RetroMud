using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using RetroMud.Core.Config;
using RetroMud.Core.Maps.CharacterColors;
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
                WormholePortalMaps = _toPortalMap(map.WormholePortalMaps),
                CharacterColors = _toCharacterColors(map.CharacterColors)
            }, jsonSettings);

            File.WriteAllLines(filePath, map.Buffer);
            File.AppendAllText(filePath, $"{ConfigConstants.MapMetaBoundary}\r\n{metaJson}");
        }

        private Dictionary<string, string> _toCharacterColors(IEnumerable<ICharacterColor> characterColors)
        {
            var dictionary = new Dictionary<string, string>();

            foreach (var group in characterColors.GroupBy(x => x.Color))
            {
                dictionary.Add(group.Key.ToString(), string.Join(",", group.Select(x => x.Character)));
            }

            return dictionary;
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
