﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RetroMud.Core.Maps.CharacterColors;

namespace RetroMud.Core.Maps.Managers
{
    public class MapFileReader : IMapFileReader
    {
        private string _boundaryDelimiter = "<!!>";

        public IMap Read(string[] fileLines)
        {
            int? boundaryLine = null;

            //split the file by the field separator
            for (var row = 0; row < fileLines.Length; row++)
            {
                if(fileLines[row].Trim() == _boundaryDelimiter)
                {
                    boundaryLine = row;
                    break;
                }
            }

            //if no boundary, the it's all map
            if (boundaryLine == null)
            {
                throw new Exception("Could not find the map boundary!");
            }

            //set the map buffer
            var buffer = new string[boundaryLine.Value];
            Array.Copy(fileLines, 0, buffer, 0, boundaryLine.Value);

            //let's grab the map meta data
            var lengthOfMeta = fileLines.Length - boundaryLine.Value - 1;
            var metaLines = new string[lengthOfMeta];

            Array.Copy(fileLines, boundaryLine.Value + 1, metaLines, 0, lengthOfMeta);

            var map = JsonConvert.DeserializeObject<Map>(string.Join("", metaLines));

            if (map.Id == 0)
            {
                throw new ArgumentNullException(nameof(map.Id));
            }
            
            var defaultCharacterColors = new List<ICharacterColor>
                {
                    new CharacterColor('@', ConsoleColor.Cyan),
                    new CharacterColor('▒', ConsoleColor.Magenta),
                    new CharacterColor('&', ConsoleColor.Red),
                };

            var characterColors = map.CharacterColors ?? defaultCharacterColors;

            return MapFactory.Create(map.Id, characterColors, buffer);
        }
    }
}