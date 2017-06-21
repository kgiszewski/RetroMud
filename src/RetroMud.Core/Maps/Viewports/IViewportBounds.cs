namespace RetroMud.Core.Maps.Viewports
{
    public interface IViewportBounds
    {
        int UpperLimit { get; }
        int LowerLimit { get; }
        int LeftLimit { get; }
        int RightLimit { get; }
    }
}
