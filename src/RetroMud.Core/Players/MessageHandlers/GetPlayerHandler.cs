using RetroMud.Core.Maps.Coordinates;
using RetroMud.Core.Players.Messages;
using RetroMud.Messaging.Dispatching;

namespace RetroMud.Core.Players.MessageHandlers
{
    public class GetPlayerHandler : IHandleTcpMessages<GetPlayerRequest, GetPlayerResponse>
    {
        public GetPlayerResponse Handle(GetPlayerRequest message)
        {
            return new GetPlayerResponse
            {
                Player = new Player
                {
                    Id = 1,
                    Position = new MapMapCoordinate(7, 54)
                }
            };
        }
    }
}
