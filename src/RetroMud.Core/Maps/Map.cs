namespace RetroMud.Core.Maps
{
    public class Map : IMap
    {
        public int Width => Data[0].Length;
        public int Height => Data.Length;
        public string[] Data { get; set; }

        public Map(int mapId)
        {
            if (mapId == 1)
            {
                Data = System.IO.File.ReadAllLines(@"..\..\..\RetroMud.Core\Maps\Data\HelloWorld.txt");
            }
        }
    }
}
