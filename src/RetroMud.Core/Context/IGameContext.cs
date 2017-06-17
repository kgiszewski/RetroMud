using RetroMud.Core.Players;
using RetroMud.Core.Scenes;
using RetroMud.Core.Status;

namespace RetroMud.Core.Context
{
    public interface IGameContext
    {
        IGameSceneManager GameSceneManager { get; set; }
        IPlayer Player { get; set; }
        IStatusMessageManager StatsStatusMessageManager { get; set; }
    }
}
