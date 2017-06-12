namespace RetroMud.Rendering.Maps
{
    public interface IRenderMaps
    {
        void RenderMap(string[] map, int rowWindowSize, int columnWindowSize, int currentColumn, int currentRow);
    }
}
