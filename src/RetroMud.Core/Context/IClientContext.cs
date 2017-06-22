using System.Collections.Generic;
using RetroMud.Core.Maps.Managers;
using RetroMud.Core.Maps.Wormholes;
using RetroMud.Core.Players;
using RetroMud.Core.Scenes;
using RetroMud.Core.Status;

namespace RetroMud.Core.Context
{
    public interface IClientContext
    {
        IGameSceneManager GameSceneManager { get; set; }
        IMapManager MapManager { get; set; }
        IPlayer Player { get; set; }
        IStatusMessageManager StatusMessageManager { get; set; }
        List<IStatusMessage> StatusMessages { get; set; }
        IWormholeManager WormholeManager { get; set; }
    }
}
