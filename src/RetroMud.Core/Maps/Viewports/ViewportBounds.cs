namespace RetroMud.Core.Maps.Viewports
{
    public class ViewportBounds : IViewportBounds
    {
        public int UpperLimit { get; }
        public int LowerLimit { get; }
        public int LeftLimit { get; }
        public int RightLimit { get; }

        public ViewportBounds(int upperLimit, int lowerLimit, int leftLimit, int rightLimit)
        {
            UpperLimit = upperLimit;
            LowerLimit = lowerLimit;
            LeftLimit = leftLimit;
            RightLimit = rightLimit;
        }
    }
}
