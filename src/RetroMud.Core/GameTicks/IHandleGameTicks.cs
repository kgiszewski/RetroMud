namespace RetroMud.Core.GameTicks
{
    public interface IHandleGameTicks
    {
        int GetFrameNumber();
        long GetLastTickLength();
    }
}
