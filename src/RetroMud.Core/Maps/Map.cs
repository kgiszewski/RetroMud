using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using RetroMud.Core.Config;
using RetroMud.Core.Context;
using RetroMud.Core.Maps.CharacterColors;

namespace RetroMud.Core.Maps
{
    public class Map : IMap
    {
        private readonly string _alteredStatePath = $"{ConfigurationManager.AppSettings[ConfigConstants.SavedStatePath]}\\{ClientContext.Instance.Player.Id}";
        private readonly string _defaultStatePath = $"..\\..\\..\\RetroMud.Core\\Maps\\Data";
        private readonly string _filename = String.Empty;

        public int Id { get; set; }
        public int Width => Data[0].Length;
        public int Height => Data.Length;
        public string[] Data { get; set; }
        public List<CharacterColor> CharacterColors { get; set; }
        public void UpdateAtPosition(int row, int column, char character)
        {
            var sb = new StringBuilder();

            sb = new StringBuilder(Data[row]) { [column] = ' ' };

            Data[row] = sb.ToString();
        }

        public void SaveAsAltered()
        {
            if (!Directory.Exists(_alteredStatePath))
            {
                Directory.CreateDirectory(_alteredStatePath);
            }

            File.WriteAllLines($"{_alteredStatePath}/{_filename}", Data);
        }

        public Map(int mapId)
        {
            Id = mapId;

            if (mapId == 1)
            {
                _filename = "HelloWorld.txt";
                Data = _getMapData();
                CharacterColors = new List<CharacterColor>
                {
                    new CharacterColor('@', ConsoleColor.Cyan),
                    new CharacterColor('▒', ConsoleColor.Red),
                };
            }

            if (mapId == 2)
            {
                _filename = "AnotherWorld.txt";
                Data = _getMapData();
                CharacterColors = new List<CharacterColor>
                {
                    new CharacterColor('@', ConsoleColor.Cyan),
                    new CharacterColor('▒', ConsoleColor.Red),
                    new CharacterColor('$', ConsoleColor.Green)
                };
            }
        }

        private string[] _getMapData()
        {
            if (File.Exists($"{_alteredStatePath}/{_filename}"))
            {
                return File.ReadAllLines($"{_alteredStatePath}/{_filename}");
            }
            else
            {
                //default map
                return File.ReadAllLines($"{_defaultStatePath}/{_filename}");
            }
        }
    }
}
