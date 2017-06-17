using RetroMud.Core.Context;
using RetroMud.Core.Events.Helpers;
using RetroMud.Core.Logging;
using RetroMud.Core.Maps.Wormholes;
using RetroMud.Core.Scenes;
using RetroMud.Core.Status.Messages;
using RetroMud.Messaging.Publishing;

namespace RetroMud.Core.Collision
{
    public class CollisionDetectionEventHandler : IRegisterClientEvents
    {
        private readonly IWormholeManager _wormholeManager;
        private readonly ISendTcpMessages _tcpMessenger;

        public CollisionDetectionEventHandler()
            : this(new WormholeManager(), new TcpMessenger())
        {
            
        }

        public CollisionDetectionEventHandler(IWormholeManager wormholeManager, ISendTcpMessages tcpMessenger)
        {
            _wormholeManager = wormholeManager;
            _tcpMessenger = tcpMessenger;
        }

        public void Register()
        {
            CollisionDetector.Instance.OnCollision += CollisionDetectionEventHandler_OnCollision;
        }

        private void CollisionDetectionEventHandler_OnCollision(object sending, CollisionDetectedEventArgs e)
        {
            Logger.Debug<CollisionDetectedHandler>($"The player has encountered a: {e.Character} character at position: {e.Column}, {e.Row}");

            if (e.Character == '▒')
            {
                _tcpMessenger.Send<AddStatusMessageResponse>(new AddStatusMessageRequest
                {
                    PlayerId = ClientContext.Instance.Player.Id,
                    Message = $"You've entered into a wormhole!"
                });

                var destinationPortal = _wormholeManager.RouteFrom(new WormholePortal
                {
                    MapId = e.Map.Id,
                    Row = e.Row,
                    Column = e.Column
                });

                if (destinationPortal != null)
                {
                    ClientContext.Instance.GameSceneManager.CurrentGameScene.IsSceneActive = false;
                    ClientContext.Instance.GameSceneManager.CurrentGameScene = new ExploreMapScene(destinationPortal.MapId);
                    ClientContext.Instance.Player.CurrentColumn = destinationPortal.Column;
                    ClientContext.Instance.Player.CurrentRow = destinationPortal.Row;
                }
            }
        }
    }
}
