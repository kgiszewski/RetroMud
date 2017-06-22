namespace RetroMud.Core.Maps.Managers
{
    public interface IMapFileWriter
    {
        void Write(IMap map, string filePath);
    }
}
