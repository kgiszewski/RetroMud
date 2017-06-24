using System.Diagnostics;
using System.Threading;
using RetroMud.Core.Config;

namespace RetroMud.Core.GameTicks
{
    public class GameTickManager : IHandleGameTicks
    {
        private int _frameNumber = 1;
        private readonly Stopwatch _frameStopwatch = Stopwatch.StartNew();
        private readonly Stopwatch _cycleStopwatch = Stopwatch.StartNew();
        private long _lastFrameLengthInMs = 0;

        public void BeginFrame()
        {
            _frameStopwatch.Restart();

            if (_frameNumber == 1)
            {
                _cycleStopwatch.Restart();
            }
        }

        public int GetFrameNumber()
        {
            return _frameNumber;
        }

        public long GetLastTickLength()
        {
            return _lastFrameLengthInMs;
        }
        
        public void EndFrame()
        {
            _lastFrameLengthInMs = _frameStopwatch.ElapsedMilliseconds;

            var minTickTime = 1000 / ConfigConstants.MaxGameFrameRate;

            if (_lastFrameLengthInMs < minTickTime)
            {
                Thread.Sleep(minTickTime - (int)_lastFrameLengthInMs);
            }

            _frameNumber++;

            if (_frameNumber == ConfigConstants.MaxGameFrameRate)
            {
                var cycleTimeInMs = _cycleStopwatch.ElapsedMilliseconds;

                if (cycleTimeInMs < 1000)
                {
                    var adjustment = 1000 - (int)cycleTimeInMs;

                    Thread.Sleep(adjustment);
                }
            }

            if (_frameNumber > ConfigConstants.MaxGameFrameRate)
            {
                _frameNumber = 1;
            }
        }
    }
}
