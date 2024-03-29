﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using RetroMud.Core.Config;
using RetroMud.Core.Context;
using RetroMud.Core.Maps;
using RetroMud.Core.Maps.Viewports;
using RetroMud.Core.NonPlayingCharacters.Animation;

namespace RetroMud.Core.Rendering
{
    public class MapRenderer : IRenderMaps
    {
        private readonly IMapViewport _mapViewport;
        private readonly IViewportBoundGenerator _boundGenerator;
        private readonly IAnimateNonPlayingCharacters _nonPlayingCharacterAnimator;
        private List<char> _colorCharacters;
        private readonly Stopwatch _stopwatch = Stopwatch.StartNew();
        private readonly Stopwatch _stopwatch2 = Stopwatch.StartNew();
        private string[] _statusArray;
        private int _totalFrames;
        private int _frameNumber;

        public MapRenderer()
            :this(new MapViewport(), new ViewportBoundGenerator(), new NonPlayingCharacterAnimator())
        {
            
        }

        public MapRenderer(
            IMapViewport mapViewport,
            IViewportBoundGenerator boundGenerator,
            IAnimateNonPlayingCharacters nonPlayingCharacterAnimator
            )
        {
            _mapViewport = mapViewport;
            _boundGenerator = boundGenerator;
            _nonPlayingCharacterAnimator = nonPlayingCharacterAnimator;
        }

        public void RenderMap(IMap map, IEnumerable<string> statusMessages)
        {
            ClientContext.Instance.GameTickManager.BeginFrame();
            _frameNumber = ClientContext.Instance.GameTickManager.GetFrameNumber();
            _totalFrames++;

            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);

            _renderFrameRate(ConfigConstants.DebugEnabled);

            var player = ClientContext.Instance.Player;

            if (_colorCharacters == null)
            {
                _colorCharacters = map.CharacterColors.Select(x => x.Character).ToList();
            }

            var bounds = _boundGenerator.GetBounds(map, _mapViewport, player.Position.Row, player.Position.Column);

            if (_statusArray == null)
            {
                _statusArray = _getStatusAsArray(statusMessages);
            }

            if (_frameNumber == ConfigConstants.MaxGameFrameRate)
            {
                _statusArray = _getStatusAsArray(statusMessages);
            }

            _nonPlayingCharacterAnimator.Animate(map);

            if (ConfigConstants.DebugEnabled)
            {
                Console.WriteLine($"Current Position: {player.Position.Row:000}, {player.Position.Row:000} UpperLimit: {bounds.UpperLimit:000} LowerLimit: {bounds.LowerLimit:000} LeftLimit: {bounds.LeftLimit:000} RightLimit: {bounds.RightLimit:000}");
                Console.WriteLine($"Map size {map.Height:000}, {map.Width:000}");
            }

            var statusRowIndex = 0;
            
            for (var row = bounds.UpperLimit; row < bounds.LowerLimit; row++)
            {
                var statusRowToRender = _getStatusRowToRender(_statusArray, statusRowIndex);
                var totalWindowWidth = _mapViewport.ColumnSize * 2;
                var spaceFiller = _getSpaceFiller(map, _mapViewport, bounds, totalWindowWidth);

                var rowToRender = $"{map.Buffer[row].Substring(bounds.LeftLimit, spaceFiller.Item2)}{spaceFiller.Item1}|{statusRowToRender}";

                if (row == player.Position.Row)
                {
                    _renderPlayerRow(rowToRender, _mapViewport, map);
                }
                else
                {
                    _renderRow(rowToRender, map);
                }

                statusRowIndex++;
            }

            _renderBlankRowsIfNeededAtBottom(map, _mapViewport, bounds);

            if (ConfigConstants.DebugEnabled)
            {
                Console.WriteLine($"RowWindow: {_mapViewport.RowSize} ColumnWindow: {_mapViewport.ColumnSize}");
            }

            ClientContext.Instance.GameTickManager.EndFrame();
        }

        private string _getStatusRowToRender(string[] statusArray, int statusRowIndex)
        {
            var statusRowToRender = statusArray.Length > statusRowIndex ? statusArray[statusRowIndex] : string.Empty;

            return statusRowToRender;
        }

        private string[] _getStatusAsArray(IEnumerable<string> statusList)
        {
            var columnWidth = 30;
            var wrappedList = new List<string>();
            var sb = new StringBuilder();

            foreach (var status in statusList)
            {
                var totalRows = Math.Ceiling((double)status.Length / columnWidth);
                var statusCharArray = status.ToCharArray();

                for (var row = 0; row < totalRows; row++)
                {
                    var textLength = 0; 

                    foreach (var c in statusCharArray.Skip(row * columnWidth).Take(columnWidth))
                    {
                        sb.Append(c);
                        textLength++;
                    }

                    for (var i = 0; i < columnWidth - textLength; i++)
                    {
                        sb.Append(' ');
                    }
                    
                    wrappedList.Add(sb.ToString());
                    sb.Clear();
                }
            }

            return wrappedList.ToArray();
        }

        private string _getBlankRow(IMap map)
        {
            var sb = new StringBuilder();

            for (var j = 0; j < map.Width; j++)
            {
                sb.Append(" ");
            }

            return sb.ToString();
        }

        private Tuple<string, int> _getSpaceFiller(IMap map, IMapViewport mapViewport, IViewportBounds bounds, int totalWindowWidth)
        {
            if (bounds.LeftLimit + totalWindowWidth > map.Width)
            {
                var sb = new StringBuilder();

                totalWindowWidth = map.Width - bounds.LeftLimit;
                sb.Clear();

                for (var i = 0; i < (mapViewport.ColumnSize * 2) - totalWindowWidth; i++)
                {
                    sb.Append(" ");
                }

                return new Tuple<string, int>(sb.ToString(), totalWindowWidth);
            }

            return new Tuple<string, int>(string.Empty, totalWindowWidth);
        }

        private void _renderBlankRowsIfNeededAtBottom(IMap map, IMapViewport mapViewport, IViewportBounds bounds)
        {
            if (ClientContext.Instance.Player.Position.Row > mapViewport.RowSize && (bounds.LowerLimit - bounds.UpperLimit < mapViewport.RowSize * 2))
            {
                var blankLineCount = (mapViewport.RowSize * 2) - (bounds.LowerLimit - bounds.UpperLimit);
                var blankRow = _getBlankRow(map);

                for (var i = 0; i < blankLineCount; i++)
                {
                    Console.WriteLine(blankRow);
                }
            }
        }

        private void _renderPlayerRow(string rowToRender, IMapViewport mapViewport, IMap map)
        {
            var column = mapViewport.ColumnSize;
            var player = ClientContext.Instance.Player;

            if (player.Position.Column < mapViewport.ColumnSize)
            {
                column = player.Position.Column;
            }

            var characterToDisplay = '@';

            if (player.IsAttacking)
            {
                switch (player.Facing)
                {
                    case Direction.East:
                    case Direction.North:
                        characterToDisplay = '/';
                        break;

                    case Direction.West:
                    case Direction.South:
                        characterToDisplay = '\\';
                        break;
                }
            }

            var sb = new StringBuilder();

            sb = new StringBuilder(rowToRender) { [column] = characterToDisplay };
            rowToRender = sb.ToString();

            _renderRow(rowToRender, map);
        }

        private void _renderRow(string rowToRender, IMap map)
        {
            if (_colorCharacters.Any(x => rowToRender.Contains(x)))
            {
                var chars = rowToRender.ToCharArray();

                var counter = 0;

                foreach (var chr in chars)
                {
                    if (_colorCharacters.Contains(chr))
                    {
                        var colorMapping = map.CharacterColors.First(x => x.Character == chr);

                        Console.ForegroundColor = colorMapping.Color;
                        Console.Write(chr);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(chr);
                    }
                    
                    counter++;
                }

                if (counter == chars.Length)
                {
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine(rowToRender);
            }
        }

        private void _renderFrameRate(bool enabled = false)
        {
            if (!enabled)
            {
                return;
            }

            if (_stopwatch.Elapsed.Seconds > 0)
            {
                var elapsedMs = 0M;

                if (ConfigConstants.MaxGameFrameRate % 2 == 0)
                {
                    elapsedMs = _stopwatch2.ElapsedMilliseconds;

                    Console.WriteLine($"Elapsed Seconds: {_stopwatch.Elapsed.Seconds:00000} Frame Number: {ClientContext.Instance.GameTickManager.GetFrameNumber():00} Frames rendered: {_totalFrames:000000} fps: {_totalFrames / _stopwatch.Elapsed.Seconds:0000} Tick Length: {ClientContext.Instance.GameTickManager.GetFrameNumber():0000}");

                    _stopwatch2.Restart();
                }
                else
                {
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine();
            }
        }
    }
}
