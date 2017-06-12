using System;
using RetroMud.Core.Healthchecks.Messages;
using RetroMud.Messaging.Dispatching;

namespace RetroMud.Core.Healthchecks.MessageHandlers
{
    public class CurrentClientVersionHandler : IHandleTcpMessages<CurrentClientVersionRequest, CurrentClientVersionResponse>
    {
        public CurrentClientVersionResponse Handle(CurrentClientVersionRequest message)
        {
            Console.WriteLine($"The current version for client Id: {message.ClientId} is {message.CurrentVersion}");

            return new CurrentClientVersionResponse
            {
                RequiresUpgrade = true
            };
        }
    }
}
