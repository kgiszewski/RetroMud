using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using RetroMud.Core.Context;
using RetroMud.Core.Maps;
using RetroMud.Core.Maps.Window;
using RetroMud.Core.Players;

namespace RetroMud.Core.Rendering
{
    public class MapRenderer : IRenderMaps
    {
        private readonly IMapWindow _mapWindow;
        private readonly IWindowBoundGenerator _boundGenerator;

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

        private List<char> _colorCharacters;

        public void RenderMap(IMap map)
        {
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);

            var player = GameContext.Instance.Player;

            _colorCharacters = map.CharacterColors.Select(x => x.Character).ToList();

            var bounds = _boundGenerator.GetBounds(map, _mapWindow, player.CurrentRow, player.CurrentColumn);

            Console.WriteLine($"Current Position: {player.CurrentRow.ToString("000")}, {player.CurrentColumn.ToString("000")} UpperLimit: {bounds.UpperLimit.ToString("000")} LowerLimit: {bounds.LowerLimit.ToString("000")} LeftLimit: {bounds.LeftLimit.ToString("000")} RightLimit: {bounds.RightLimit.ToString("000")}");
            Console.WriteLine($"Map size {map.Height.ToString("000")}, {map.Width.ToString("000")}");

            for (var row = bounds.UpperLimit; row < bounds.LowerLimit; row++)
            {
                var totalWindowWidth = _mapWindow.ColumnSize * 2;
                var spaceFiller = _getSpaceFiller(map, _mapWindow, bounds, totalWindowWidth);

                var renderedRow = $"{map.Data[row].Substring(bounds.LeftLimit, spaceFiller.Item2)}{spaceFiller.Item1}";

                if (row == player.CurrentRow)
                {
                    _renderPlayerRow(renderedRow, _mapWindow, player, map);
                }
                else
                {
                    _renderRow(renderedRow, map);
                }
            }

            _renderBlankRowsIfNeededAtBottom(map, player, _mapWindow, bounds);

            Console.WriteLine($"RowWindow: {_mapWindow.RowSize} ColumnWindow: {_mapWindow.ColumnSize} ");
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

        private void _renderBlankRowsIfNeededAtBottom(IMap map, IPlayer player, IMapWindow mapWindow, IWindowBounds bounds)
        {
            if (player.CurrentRow > mapWindow.RowSize && (bounds.LowerLimit - bounds.UpperLimit < mapWindow.RowSize * 2))
            {
                var blankLineCount = (mapWindow.RowSize * 2) - (bounds.LowerLimit - bounds.UpperLimit);
                var blankRow = _getBlankRow(map);

                for (var i = 0; i < blankLineCount; i++)
                {
                    Console.WriteLine(blankRow);
                }
            }
        }

        private void _renderPlayerRow(string rowToRender, IMapWindow mapWindow, IPlayer player, IMap map)
        {
            var column = mapWindow.ColumnSize;

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

                    if (counter == chars.Length)
                    {
                        Console.WriteLine();
                    }
                }
            }
            else
            {
                Console.WriteLine(rowToRender);
            }
        }
    }
}
