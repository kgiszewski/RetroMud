namespace RetroMud.Core.Scenes
{
    public class GameSceneManager : IGameSceneManager
    {
        public IGameScene CurrentGameScene { get; internal set; }
        private IGameScene _nextGameScene;
        private IGameScene _previousGameScene;

        private void _changeToNextScene()
        {
            if (CurrentGameScene != null)
            {
                CurrentGameScene.IsSceneActive = false;
            }

            CurrentGameScene = _nextGameScene;
            CurrentGameScene.IsSceneActive = true;
        }

        public void ChangeToNextScene(IGameScene scene)
        {
            _nextGameScene = scene;
            _changeToNextScene();
        }

        public void OpenModalScene(IGameScene scene)
        {
            CurrentGameScene.IsSceneActive = false;
            _previousGameScene = CurrentGameScene;
            _nextGameScene = scene;
            _changeToNextScene();
        }

        public void CloseModalScene()
        {
            CurrentGameScene.IsSceneActive = false;
            _nextGameScene = _previousGameScene;
            _changeToNextScene();
        }
    }
}
