﻿using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using RetroMud.Core.Config;
using RetroMud.Core.Context;

namespace RetroMud.Core.Maps.Managers
{
    public class FileSystemMapManager : IMapManager
    {
        private readonly IMapFileReader _mapFileReader;
        private readonly string _alteredStatePath = $"{ConfigurationManager.AppSettings[ConfigConstants.SavedStatePath]}\\{ClientContext.Instance.Player.Id}";
        private readonly string _defaultStatePath = $"..\\..\\..\\RetroMud.Core\\Maps\\Data";
        private List<FileSystemMap> _allMaps;

        public FileSystemMapManager()
            :this(new MapFileReader())
        {
            
        }

        public FileSystemMapManager(IMapFileReader mapFileReader)
        {
            _mapFileReader = mapFileReader;
        }

        public void SaveAsAltered(IMap map)
        {
            var fileSystemMap = _allMaps.First(x => x.Map.Id == map.Id);

            if (!Directory.Exists(_alteredStatePath))
            {
                Directory.CreateDirectory(_alteredStatePath);
            }

            File.WriteAllLines($"{_alteredStatePath}/{fileSystemMap.FileName}", map.Buffer);
        }

        public IEnumerable<IMap> GetAllMaps()
        {
            if (_allMaps == null)
            {
                _allMaps = new List<FileSystemMap>();
                
                foreach (var mapFile in Directory.GetFiles(_defaultStatePath))
                {
                    var fileName = Path.GetFileName(mapFile);

                    _allMaps.Add(new FileSystemMap
                    {
                        FileName = fileName,
                        Map = _mapFileReader.Read(_getMapData(fileName))
                    });
                }
            }

            return _allMaps.Select(x => x.Map);
        }

        public IMap GetById(int mapId)
        {
            return GetAllMaps().First(x => x.Id == mapId);
        }

        private string[] _getMapData(string fileName)
        {
            if (File.Exists($"{_alteredStatePath}/{fileName}"))
            {
                return File.ReadAllLines($"{_alteredStatePath}/{fileName}");
            }
            else
            {
                //default map
                return File.ReadAllLines($"{_defaultStatePath}/{fileName}");
            }
        }
    }
}
