using RetroMud.Core.Players;
using RetroMud.Core.Scenes;
using RetroMud.Core.Status;

namespace RetroMud.Core.Context
{
    public interface IClientContext
    {
        IGameSceneManager GameSceneManager { get; set; }
        IPlayer Player { get; set; }
        IStatusMessageManager StatusMessageManager { get; set; }
    }
}
