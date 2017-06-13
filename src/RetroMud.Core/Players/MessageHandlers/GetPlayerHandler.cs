﻿using RetroMud.Core.Players.Messages;
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
                    CurrentRow = 7,
                    CurrentColumn = 54
                }
            };
        }
    }
}