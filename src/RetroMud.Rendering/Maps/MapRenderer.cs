using System;
using System.Text;
using RetroMud.Core.Maps;
using RetroMud.Core.Maps.Window;

namespace RetroMud.Rendering.Maps
{
    public class MapRenderer : IRenderMaps
    {
        public void RenderMap(IMap map, IMapWindow mapWindow, IWindowBoundGenerator boundGenerator, int currentColumn, int currentRow)
        {
            Console.SetCursorPosition(0, 0);

            var sb = new StringBuilder();

            for (var j = 0; j < map.Width; j++)
            {
                sb.Append(" ");
            }

            var blankRow = sb.ToString();

            var bounds = boundGenerator.GetBounds(map, mapWindow, currentRow, currentColumn);

            Console.WriteLine($"Current Position: {currentRow.ToString("00")}, {currentColumn.ToString("00")} UpperLimit: {bounds.UpperLimit.ToString("00")} LowerLimit: {bounds.LowerLimit.ToString("00")} LeftLimit: {bounds.LeftLimit.ToString("00")} RightLimit: {bounds.RightLimit.ToString("00")}");
            Console.WriteLine($"Map size {map.Height.ToString("00")}, {map.Width.ToString("00")}");

            for (var row = bounds.UpperLimit; row < bounds.LowerLimit; row++)
            {
                var width = mapWindow.ColumnSize * 2;
                var spaceFiller = string.Empty;

                if (bounds.LeftLimit + width > map.Width)
                {
                    width = map.Width - bounds.LeftLimit;
                    sb.Clear();

                    for (var i = 0; i < (mapWindow.ColumnSize * 2) - width; i++)
                    {
                        sb.Append(" ");
                    }

                    spaceFiller = sb.ToString();
                }

                var renderedRow = $"{map.Data[row].Substring(bounds.LeftLimit, width)}{spaceFiller}";

                if (row == currentRow)
                {
                    var column = mapWindow.ColumnSize;

                    if (currentColumn < mapWindow.ColumnSize)
                    {
                        column = currentColumn;
                    }

                    sb = new StringBuilder(renderedRow) { [column] = '@' };
                    renderedRow = sb.ToString();

                    WriteLineWithColoredLetter(renderedRow, '@');
                    continue;
                }

                Console.WriteLine(renderedRow);
            }

            if (currentRow > mapWindow.RowSize && (bounds.LowerLimit - bounds.UpperLimit < mapWindow.RowSize * 2))
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
    }
}
