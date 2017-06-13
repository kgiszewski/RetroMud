namespace RetroMud.Core.Maps.Window
{
    public class WindowBounds : IWindowBounds
    {
        public int UpperLimit { get; }
        public int LowerLimit { get; }
        public int LeftLimit { get; }
        public int RightLimit { get; }

        public WindowBounds(int upperLimit, int lowerLimit, int leftLimit, int rightLimit)
        {
            UpperLimit = upperLimit;
            LowerLimit = lowerLimit;
            LeftLimit = leftLimit;
            RightLimit = rightLimit;
        }
    }
}
