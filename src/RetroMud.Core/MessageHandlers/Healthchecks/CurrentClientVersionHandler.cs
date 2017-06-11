using System;
using RetroMud.Messaging.Dispatching;
using RetroMud.Messaging.Messages.Healthchecks;

namespace RetroMud.Core.MessageHandlers.Healthchecks
{
    public class CurrentClientVersionHandler : IHandleTcpMessages<CurrentClientVersion, CurrentClientVersionResponse>
    {
        public CurrentClientVersionResponse Handle(CurrentClientVersion message)
        {
            Console.WriteLine($"The current version for client Id: {message.ClientId} is {message.CurrentVersion}");

            return new CurrentClientVersionResponse
            {
                RequiresUpgrade = true
            };
        }
    }
}
