using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RetroMud.Core.Context;
using RetroMud.Core.Maps;
using RetroMud.Core.Maps.Window;

namespace RetroMud.Core.Rendering
{
    public class MapRenderer : IRenderMaps
    {
        private readonly IMapWindow _mapWindow;
        private readonly IWindowBoundGenerator _boundGenerator;
        private List<char> _colorCharacters;

        public MapRenderer()
            :this(new MapWindow(), new WindowBoundGenerator())
        {
            
        }

        public MapRenderer(
            IMapWindow mapWindow,
            IWindowBoundGenerator boundGenerator
            )
        {
            _mapWindow = mapWindow;
            _boundGenerator = boundGenerator;
        }

        public void RenderMap(IMap map, IEnumerable<string> statusMessages)
        {
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);

            var player = ClientContext.Instance.Player;

            if (_colorCharacters == null)
            {
                _colorCharacters = map.CharacterColors.Select(x => x.Character).ToList();
            }

            var bounds = _boundGenerator.GetBounds(map, _mapWindow, player.CurrentRow, player.CurrentColumn);

            //Console.WriteLine($"Current Position: {player.CurrentRow.ToString("000")}, {player.CurrentColumn.ToString("000")} UpperLimit: {bounds.UpperLimit.ToString("000")} LowerLimit: {bounds.LowerLimit.ToString("000")} LeftLimit: {bounds.LeftLimit.ToString("000")} RightLimit: {bounds.RightLimit.ToString("000")}");
            //Console.WriteLine($"Map size {map.Height.ToString("000")}, {map.Width.ToString("000")}");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write($"Gold: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"{ClientContext.Instance.Player.Gold:00000000}");
            Console.WriteLine();
            Console.WriteLine();
            Console.ResetColor();

            var statusArray = _getStatusAsArray(statusMessages);

            var statusRowIndex = 0;
            
            for (var row = bounds.UpperLimit; row < bounds.LowerLimit; row++)
            {
                var statusRowToRender = _getStatusRowToRender(statusArray, statusRowIndex);
                var totalWindowWidth = _mapWindow.ColumnSize * 2;
                var spaceFiller = _getSpaceFiller(map, _mapWindow, bounds, totalWindowWidth);

                var rowToRender = $"{map.Data[row].Substring(bounds.LeftLimit, spaceFiller.Item2)}{spaceFiller.Item1}|{statusRowToRender}";

                if (row == player.CurrentRow)
                {
                    _renderPlayerRow(rowToRender, _mapWindow, map);
                }
                else
                {
                    _renderRow(rowToRender, map);
                }

                statusRowIndex++;
            }

            _renderBlankRowsIfNeededAtBottom(map, _mapWindow, bounds);

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

        private Tuple<string, int> _getSpaceFiller(IMap map, IMapWindow mapWindow, IWindowBounds bounds, int totalWindowWidth)
        {
            if (bounds.LeftLimit + totalWindowWidth > map.Width)
            {
                var sb = new StringBuilder();

                totalWindowWidth = map.Width - bounds.LeftLimit;
                sb.Clear();

                for (var i = 0; i < (mapWindow.ColumnSize * 2) - totalWindowWidth; i++)
                {
                    sb.Append(" ");
                }

                return new Tuple<string, int>(sb.ToString(), totalWindowWidth);
            }

            return new Tuple<string, int>(string.Empty, totalWindowWidth);
        }

        private void _renderBlankRowsIfNeededAtBottom(IMap map, IMapWindow mapWindow, IWindowBounds bounds)
        {
            if (ClientContext.Instance.Player.CurrentRow > mapWindow.RowSize && (bounds.LowerLimit - bounds.UpperLimit < mapWindow.RowSize * 2))
            {
                var blankLineCount = (mapWindow.RowSize * 2) - (bounds.LowerLimit - bounds.UpperLimit);
                var blankRow = _getBlankRow(map);

                for (var i = 0; i < blankLineCount; i++)
                {
                    Console.WriteLine(blankRow);
                }
            }
        }

        private void _renderPlayerRow(string rowToRender, IMapWindow mapWindow, IMap map)
        {
            var column = mapWindow.ColumnSize;
            var player = ClientContext.Instance.Player;

            if (player.CurrentColumn < mapWindow.ColumnSize)
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
    }
}
