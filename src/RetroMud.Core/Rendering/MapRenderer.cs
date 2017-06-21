using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using RetroMud.Core.Context;
using RetroMud.Core.Maps;
using RetroMud.Core.Maps.Viewports;

namespace RetroMud.Core.Rendering
{
    public class MapRenderer : IRenderMaps
    {
        private readonly IMapViewport _mapViewport;
        private readonly IViewportBoundGenerator _boundGenerator;
        private List<char> _colorCharacters;
        private int _frames;
        private Stopwatch _stopwatch;

        public MapRenderer()
            :this(new MapViewport(), new ViewportBoundGenerator())
        {
            
        }

        public MapRenderer(
            IMapViewport mapViewport,
            IViewportBoundGenerator boundGenerator
            )
        {
            _mapViewport = mapViewport;
            _boundGenerator = boundGenerator;
            _stopwatch = Stopwatch.StartNew();
        }

        public void RenderMap(IMap map, IEnumerable<string> statusMessages)
        {
            _frames++;
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);

            _renderFrameRate(true);

            var player = ClientContext.Instance.Player;

            if (_colorCharacters == null)
            {
                _colorCharacters = map.CharacterColors.Select(x => x.Character).ToList();
            }

            var bounds = _boundGenerator.GetBounds(map, _mapViewport, player.CurrentRow, player.CurrentColumn);

            //Console.WriteLine($"Current Position: {player.CurrentRow.ToString("000")}, {player.CurrentColumn.ToString("000")} UpperLimit: {bounds.UpperLimit.ToString("000")} LowerLimit: {bounds.LowerLimit.ToString("000")} LeftLimit: {bounds.LeftLimit.ToString("000")} RightLimit: {bounds.RightLimit.ToString("000")}");
            //Console.WriteLine($"Map size {map.Height.ToString("000")}, {map.Width.ToString("000")}");

            var statusArray = _getStatusAsArray(statusMessages);

            var statusRowIndex = 0;
            
            for (var row = bounds.UpperLimit; row < bounds.LowerLimit; row++)
            {
                var statusRowToRender = _getStatusRowToRender(statusArray, statusRowIndex);
                var totalWindowWidth = _mapViewport.ColumnSize * 2;
                var spaceFiller = _getSpaceFiller(map, _mapViewport, bounds, totalWindowWidth);

                var rowToRender = $"{map.Buffer[row].Substring(bounds.LeftLimit, spaceFiller.Item2)}{spaceFiller.Item1}|{statusRowToRender}";

                if (row == player.CurrentRow)
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

            //Console.WriteLine($"RowWindow: {_mapWindow.RowSize} ColumnWindow: {_mapWindow.ColumnSize}");
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
            if (ClientContext.Instance.Player.CurrentRow > mapViewport.RowSize && (bounds.LowerLimit - bounds.UpperLimit < mapViewport.RowSize * 2))
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

            if (player.CurrentColumn < mapViewport.ColumnSize)
            {
                column = player.CurrentColumn;
            }

            var sb = new StringBuilder();

            sb = new StringBuilder(rowToRender) { [column] = '@' };
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
                Console.WriteLine($"Frames rendered: {_frames:000000} fps: {_frames / _stopwatch.Elapsed.Seconds:00}");
            }
            else
            {
                Console.WriteLine();
            }
        }
    }
}
