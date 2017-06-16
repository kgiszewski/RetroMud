using RetroMud.Core.Players;
using RetroMud.Core.Scenes;

namespace RetroMud.Core.Context
{
    public interface IGameContext
    {
        IGameSceneManager GameSceneManager { get; set; }
        IPlayer Player { get; set; }
    }
}
