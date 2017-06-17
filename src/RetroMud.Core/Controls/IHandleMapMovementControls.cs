using RetroMud.Core.Maps;
using RetroMud.Core.Players;

namespace RetroMud.Core.Controls
{
    public interface IHandleMapMovementControls
    {
        void HandleInput(IMap map, IPlayer player);
    }
}
