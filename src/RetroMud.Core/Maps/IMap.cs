namespace RetroMud.Core.Maps
{
    public interface IMap
    {
        int Width { get; }
        int Height { get; }
        string[] Data { get; set; }
    }
}
