namespace RetroMud.Core.Maps.IO
{
    public interface IHandleMapIo
    {
        IMap LoadMap();
        void SaveAlteredMapState();
    }
}
