namespace RetroMud.Core.Maps.Managers
{
    public interface IMapFileReader
    {
        IMap Read(string[] fileLines);
    }
}
