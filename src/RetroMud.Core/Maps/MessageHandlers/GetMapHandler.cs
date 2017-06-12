using RetroMud.Core.Maps.Messages;
using RetroMud.Messaging.Dispatching;

namespace RetroMud.Core.Maps.MessageHandlers
{
    public class GetMapHandler : IHandleTcpMessages<GetMapRequest, GetMapResponse>
    {
        public GetMapResponse Handle(GetMapRequest request)
        {
            return new GetMapResponse
            {
                Map = new Map(request.MapId)
            };
        }
    }
}
