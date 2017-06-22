﻿using RetroMud.Core.Collision.Dispatching;
using RetroMud.Core.Context;
using RetroMud.Core.Maps.Coordinates;
using RetroMud.Core.Maps.Wormholes;
using RetroMud.Core.Scenes;
using RetroMud.Core.Status;

namespace RetroMud.Core.Collision.CollisionHandlers
{
    [CollisionCharacter('▒')]
    public class WormholeHandler : IHandleCharacterCollisions
    {
        private readonly IWormholeManager _wormholeManager;
        private readonly IStatusMessageManager _statusMessageManager;

        public WormholeHandler()
            :this(new WormholeManager(), new StatusMessageManager())
        {
            
        }

        public WormholeHandler(IWormholeManager wormholeManager, IStatusMessageManager statusMessageManager)
        {
            _wormholeManager = wormholeManager;
            _statusMessageManager = statusMessageManager;
        }

        public void Handle(CollisionDetectedEventArgs eventArgs)
        {
            _statusMessageManager.AddStatusMessage("You've entered a wormhole!");

            var destinationPortal = _wormholeManager.RouteFrom(new WormholePortal
            {
                MapId = eventArgs.Map.Id,
                Position = new MapCoordinate(eventArgs.Position.Row, eventArgs.Position.Column)
            });

            if (destinationPortal != null)
            {
                ClientContext.Instance.MapManager.SaveAsAltered(eventArgs.Map);

                ClientContext.Instance.GameSceneManager.ChangeToNextScene(new ExploreMapScene(destinationPortal.MapId));

                //passing by value so that we don't mess with the portal mappings
                ClientContext.Instance.Player.Position.Row = destinationPortal.Position.Row;
                ClientContext.Instance.Player.Position.Column = destinationPortal.Position.Column;
            }
        }
    }
}
