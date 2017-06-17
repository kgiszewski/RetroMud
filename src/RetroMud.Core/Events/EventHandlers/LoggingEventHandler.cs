using RetroMud.Core.Events.Helpers;
using RetroMud.Core.Logging;
using RetroMud.Messaging.Dispatching;

namespace RetroMud.Core.Events.EventHandlers
{
    public class LoggingEventHandler : IRegisterServerEvents
    {
        public void Register()
        {
            MessageDispatcher.Instance.OnDispatchMessage += LoggingEventHandler_OnDispatchMessage;
        }

        private void LoggingEventHandler_OnDispatchMessage(object sender, DispatcherEventArgs e)
        {
            Logger.Debug<LoggingEventHandler>($"Dispatching {e.Message.MessageType} to {e.HandlerType.Name}...");
        }
    }
}
