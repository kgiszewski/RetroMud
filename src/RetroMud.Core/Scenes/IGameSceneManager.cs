namespace RetroMud.Core.Scenes
{
    public interface IGameSceneManager
    {
        IGameScene CurrentGameScene { get; set; }
        void ChangeToNextScene(IGameScene scene);
        void OpenModalScene(IGameScene scene);
        void CloseModalScene();
    }
}
