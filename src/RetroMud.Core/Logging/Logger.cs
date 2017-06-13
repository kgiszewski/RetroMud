using log4net;

namespace RetroMud.Core.Logging
{
    public static class Logger
    {
        public static void Error<T>(string message)
        {
            var log = LogManager.GetLogger(typeof(T));

            log.Error(message);
        }

        public static void Debug<T>(string message)
        {
            var log = LogManager.GetLogger(typeof(T));

            log.Debug(message);
        }

        public static void Warn<T>(string message)
        {
            var log = LogManager.GetLogger(typeof(T));

            log.Warn(message);
        }

        public static void Info<T>(string message)
        {
            var log = LogManager.GetLogger(typeof(T));

            log.Info(message);
        }
    }
}
