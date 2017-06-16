namespace RetroMud.Core.Scenes
{
    public class GameSceneManager : IGameSceneManager
    {
        public IGameScene CurrentGameScene { get; set; }
    }
}
