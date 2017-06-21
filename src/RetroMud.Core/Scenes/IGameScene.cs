namespace RetroMud.Core.Scenes
{
    public interface IGameScene
    {
        void Setup();
        void Render();
        bool IsSceneActive { get; set; }
    }
}
