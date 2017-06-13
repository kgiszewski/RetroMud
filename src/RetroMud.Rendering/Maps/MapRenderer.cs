using System;
using System.Text;
using RetroMud.Core.Maps;
using RetroMud.Core.Maps.Window;
using RetroMud.Core.Players;

namespace RetroMud.Rendering.Maps
{
    public class MapRenderer : IRenderMaps
    {
        public void RenderMap(IMap map, IMapWindow mapWindow, IWindowBoundGenerator boundGenerator, IPlayer player)
        {
            Console.SetCursorPosition(0, 0);

            var blankRow = _getBlankRow(map);

            var bounds = boundGenerator.GetBounds(map, mapWindow, player.CurrentRow, player.CurrentColumn);

            Console.WriteLine($"Current Position: {player.CurrentRow.ToString("000")}, {player.CurrentColumn.ToString("000")} UpperLimit: {bounds.UpperLimit.ToString("000")} LowerLimit: {bounds.LowerLimit.ToString("000")} LeftLimit: {bounds.LeftLimit.ToString("000")} RightLimit: {bounds.RightLimit.ToString("000")}");
            Console.WriteLine($"Map size {map.Height.ToString("000")}, {map.Width.ToString("000")}");

            for (var row = bounds.UpperLimit; row < bounds.LowerLimit; row++)
            {
                var totalWindowWidth = mapWindow.ColumnSize * 2;
                var spaceFiller = _getSpaceFiller(map, mapWindow, bounds, totalWindowWidth);

                var renderedRow = $"{map.Data[row].Substring(bounds.LeftLimit, spaceFiller.Item2)}{spaceFiller.Item1}";

                if (row == player.CurrentRow)
                {
                    var column = mapWindow.ColumnSize;

                    if (player.CurrentColumn < mapWindow.ColumnSize)
                    {
                        column = player.CurrentColumn;
                    }

                    var sb = new StringBuilder();

                    sb = new StringBuilder(renderedRow) { [column] = '@' };
                    renderedRow = sb.ToString();

                    WriteLineWithColoredLetter(renderedRow, '@');
                    continue;
                }

                Console.WriteLine(renderedRow);
            }

            if (player.CurrentRow > mapWindow.RowSize && (bounds.LowerLimit - bounds.UpperLimit < mapWindow.RowSize * 2))
            {
                var blankLineCount = (mapWindow.RowSize * 2) - (bounds.LowerLimit - bounds.UpperLimit);

                for (var i = 0; i < blankLineCount; i++)
                {
                    Console.WriteLine(blankRow);
                }
            }

            Console.WriteLine($"RowWindow: {mapWindow.RowSize} ColumnWindow: {mapWindow.ColumnSize} ");
        }

        void WriteLineWithColoredLetter(string letters, char c)
        {
            var o = letters.IndexOf(c);
            Console.Write(letters.Substring(0, o));
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(letters[o]);
            Console.ResetColor();
            Console.WriteLine(letters.Substring(o + 1));
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
    }
}
