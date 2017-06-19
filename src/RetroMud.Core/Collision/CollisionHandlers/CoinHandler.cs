using System.Text;
using RetroMud.Core.Collision.Dispatching;
using RetroMud.Core.Context;
using RetroMud.Core.Status;

namespace RetroMud.Core.Collision.CollisionHandlers
{
    [CollisionCharacter('$')]
    public class CoinHandler : IHandleCharacterCollisions
    {
        private readonly IStatusMessageManager _statusMessageManager;

        public CoinHandler()
            : this(new StatusMessageManager())
        {
            
        }

        public CoinHandler(IStatusMessageManager statusMessageManager)
        {
            _statusMessageManager = statusMessageManager;
        }

        public void Handle(CollisionDetectedEventArgs eventArgs)
        {
            _statusMessageManager.AddStatusMessage(ClientContext.Instance.Player, "+1 Coin!");

            eventArgs.Map.UpdateAtPosition(eventArgs.Row, eventArgs.Column, ' ');
        }
    }
}
