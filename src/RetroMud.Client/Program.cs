using System;
using System.Text;
using RetroMud.Messaging.Publishing;

namespace RetroMud
{
    class Program
    {
        private static readonly Random Rand = new Random();
        private static ISendTcpMessages _messenger;

        static void Main(string[] args)
        {
            Console.WindowWidth = 153;
            Console.WindowHeight = 40;

            var clientId = Rand.Next(123123123);
            var clientVersion = "0.1.0";

            //_messenger = TcpMessengerFactory.GetMessenger();

            //Console.WriteLine("Sending healthcheck info...");

            //var response = ((CurrentClientVersionResponse)_messenger.Send(new CurrentClientVersion
            //{
            //    ClientId = clientId,
            //    CurrentVersion = clientVersion
            //}));

            //Console.WriteLine($"Requires upgrade: {response.RequiresUpgrade}");

            var map = System.IO.File.ReadAllLines(@"map1.txt");

            var rowWindowSize = 10;
            var columnWindowSize = 40;
            var currentColumn = 10;
            var currentRow = 6;
            var mapWidth = map[0].Length;
            var mapHeight = map.Length;
            
            _renderMap(map, rowWindowSize, columnWindowSize, ref currentColumn, ref currentRow);

            while (true)
            {
                var input = Console.ReadKey(true);

                if (input.KeyChar == 'a' && currentColumn > 0)
                {
                    currentColumn--;
                }

                if (input.KeyChar == 'w' && currentRow > 0)
                {
                    currentRow--;
                }

                if (input.KeyChar == 'd' && currentColumn < mapWidth)
                {
                    currentColumn++;
                }

                if (input.KeyChar == 's' && currentRow < mapHeight)
                {
                    currentRow++;
                }

                _renderMap(map, rowWindowSize, columnWindowSize, ref currentColumn, ref currentRow);
            }
        }

        private static void _renderMap(string [] map, int rowWindowSize, int columnWindowSize, ref int currentColumn, ref int currentRow)
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

                Console.WriteLine($"{map[row].Substring(leftLimit, width)}{spaceFiller}");
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
    }
}
