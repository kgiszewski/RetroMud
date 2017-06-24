using System.Diagnostics;
using System.Threading;
using RetroMud.Core.Config;

namespace RetroMud.Core.GameTicks
{
    public class GameTickManager : IHandleGameTicks
    {
        private int _frameNumber = 1;
        private readonly Stopwatch _stopwatch = Stopwatch.StartNew();
        private readonly Stopwatch _cycleStopwatch = Stopwatch.StartNew();
        private long _lastTickLengthInMs = 0;

        public int GetFrameNumber()
        {
            _advanceGameTick();

            if (_frameNumber > ConfigConstants.MaxGameFrameRate)
            {
                _frameNumber = 1;
            }

            return _frameNumber;
        }

        public long GetLastTickLength()
        {
            return _lastTickLengthInMs;
        }

        private void _advanceGameTick()
        {
            _lastTickLengthInMs = _stopwatch.ElapsedMilliseconds;
            _stopwatch.Restart();

            var minTickTime = 1000 / ConfigConstants.MaxGameFrameRate;

            if (_lastTickLengthInMs < minTickTime)
            {
                Thread.Sleep(minTickTime - (int)_lastTickLengthInMs);
            }

            _frameNumber++;

            if (_frameNumber == ConfigConstants.MaxGameFrameRate)
            {
                var cycleTimeInMs = _cycleStopwatch.ElapsedMilliseconds;
                _cycleStopwatch.Restart();

                if (cycleTimeInMs < 1000)
                {
                    var adjustment = 1000 - (int) cycleTimeInMs;

                    Thread.Sleep(adjustment);
                }
            }
        }
    }
}
