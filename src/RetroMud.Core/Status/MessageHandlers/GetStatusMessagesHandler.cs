using System;
using System.Collections.Generic;
using RetroMud.Core.Status.Messages;
using RetroMud.Messaging.Dispatching;

namespace RetroMud.Core.Status.MessageHandlers
{
    public class GetStatusMessagesHandler : IHandleTcpMessages<GetStatusMessagesRequest, GetStatusMessagesResponse>
    {
        public GetStatusMessagesResponse Handle(GetStatusMessagesRequest message)
        {
            return new GetStatusMessagesResponse
            {
                StatusMessages = new List<StatusMessage>
                {
                    new StatusMessage
                    {
                        Message = "This is the first message and it should wrap!",
                        CreatedOn = DateTime.Now
                    },
                    new StatusMessage
                    {
                        Message = "This is the second message and it should wrap!",
                        CreatedOn = DateTime.Now.AddMinutes(-1)
                    }
                }
            };
        }
    }
}
