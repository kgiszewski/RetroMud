using System;
using System.Text;

namespace RetroMud.Rendering.Maps
{
    public class MapRenderer : IRenderMaps
    {
        public void RenderMap(string[] map, int rowWindowSize, int columnWindowSize, int currentColumn, int currentRow)
        {
            Console.SetCursorPosition(0, 0);
            var mapWidth = map[0].Length;
            var mapHeight = map.Length;

            var sb = new StringBuilder();

            for (var j = 0; j < mapWidth; j++)
            {
                sb.Append(" ");
            }

            var blankRow = sb.ToString();

            var leftLimit = currentColumn - columnWindowSize;
            if (leftLimit < 0)
            {
                leftLimit = 0;
            }

            var rightLimit = currentColumn + columnWindowSize;
            if (rightLimit > mapWidth)
            {
                rightLimit = mapWidth - columnWindowSize;
            }

            var upperLimit = currentRow - rowWindowSize;
            if (upperLimit < 0)
            {
                upperLimit = 0;
            }

            var lowerLimit = currentRow + rowWindowSize;
            if (lowerLimit > mapHeight)
            {
                lowerLimit = mapHeight;
            }

            if (lowerLimit < rowWindowSize * 2)
            {
                lowerLimit = rowWindowSize * 2;
            }

            Console.WriteLine($"Current Position: {currentRow.ToString("00")}, {currentColumn.ToString("00")} UpperLimit: {upperLimit.ToString("00")} LowerLimit: {lowerLimit.ToString("00")} LeftLimit: {leftLimit.ToString("00")} RightLimit: {rightLimit.ToString("00")}");
            Console.WriteLine($"Map size {mapHeight.ToString("00")}, {mapWidth.ToString("00")}");

            for (var row = upperLimit; row < lowerLimit; row++)
            {
                var width = columnWindowSize * 2;
                var spaceFiller = string.Empty;

                if (leftLimit + width > mapWidth)
                {
                    width = mapWidth - leftLimit;
                    sb.Clear();

                    for (var i = 0; i < (columnWindowSize * 2) - width; i++)
                    {
                        sb.Append(" ");
                    }

                    spaceFiller = sb.ToString();
                }

                var renderedRow = $"{map[row].Substring(leftLimit, width)}{spaceFiller}";

                if (row == currentRow)
                {
                    var column = columnWindowSize;

                    if (currentColumn < columnWindowSize)
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

            if (currentRow > rowWindowSize && (lowerLimit - upperLimit < rowWindowSize * 2))
            {
                var blankLineCount = (rowWindowSize * 2) - (lowerLimit - upperLimit);

                for (var i = 0; i < blankLineCount; i++)
                {
                    Console.WriteLine(blankRow);
                }
            }

            Console.WriteLine($"RowWindow: {rowWindowSize} ColumnWindow: {columnWindowSize} ");
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
