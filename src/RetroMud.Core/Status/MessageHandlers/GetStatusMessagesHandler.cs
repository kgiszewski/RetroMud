using System.Linq;
using RetroMud.Core.Context;
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
                StatusMessages = InstanceContext.Instance.StatusMessages.Where(x => x.PlayerId == message.PlayerId).ToList()
            };
        }
    }
}
