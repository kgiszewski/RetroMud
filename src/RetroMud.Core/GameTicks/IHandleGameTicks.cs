namespace RetroMud.Core.GameTicks
{
    public interface IHandleGameTicks
    {
        void BeginFrame();
        int GetFrameNumber();
        long GetLastTickLength();
        void EndFrame();
    }
}
