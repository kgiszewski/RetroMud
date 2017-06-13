namespace RetroMud.Core.Maps.Window
{
    public interface IWindowBounds
    {
        int UpperLimit { get; }
        int LowerLimit { get; }
        int LeftLimit { get; }
        int RightLimit { get; }
    }
}
