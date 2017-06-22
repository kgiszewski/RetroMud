using RetroMud.Core.Collision.Dispatching;
using RetroMud.Core.Context;
using RetroMud.Core.Maps.Managers;
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
        private readonly IMapManager _mapManager;

        public WormholeHandler()
            :this(new WormholeManager(), new StatusMessageManager(), new FileSystemMapManager())
        {
            
        }

        public WormholeHandler(IWormholeManager wormholeManager, IStatusMessageManager statusMessageManager, IMapManager mapManager)
        {
            _wormholeManager = wormholeManager;
            _statusMessageManager = statusMessageManager;
            _mapManager = mapManager;
        }

        public void Handle(CollisionDetectedEventArgs eventArgs)
        {
            _statusMessageManager.AddStatusMessage("You've entered a wormhole!");

            var destinationPortal = _wormholeManager.RouteFrom(new WormholePortal
            {
                MapId = eventArgs.Map.Id,
                Row = eventArgs.Row,
                Column = eventArgs.Column
            });

            if (destinationPortal != null)
            {
                _mapManager.SaveAsAltered(eventArgs.Map);

                ClientContext.Instance.GameSceneManager.ChangeToNextScene(new ExploreMapScene(destinationPortal.MapId));
                ClientContext.Instance.Player.Position.Column = destinationPortal.Column;
                ClientContext.Instance.Player.Position.Row = destinationPortal.Row;
            }
        }
    }
}
