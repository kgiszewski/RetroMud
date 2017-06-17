using System;
using RetroMud.Core.Context;
using RetroMud.Core.Status.Messages;
using RetroMud.Messaging.Dispatching;

namespace RetroMud.Core.Status.MessageHandlers
{
    public class AddStatusMessageHandler : IHandleTcpMessages<AddStatusMessageRequest, AddStatusMessageResponse>
    {
        public AddStatusMessageResponse Handle(AddStatusMessageRequest message)
        {
            InstanceContext.Instance.StatusMessages.Add(new StatusMessage
            {
                PlayerId = message.PlayerId,
                Message = message.Message,
                CreatedOn = DateTime.Now
            });

            return new AddStatusMessageResponse();
        }
    }
}
