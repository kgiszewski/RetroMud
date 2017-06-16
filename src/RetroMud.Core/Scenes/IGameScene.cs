namespace RetroMud.Core.Scenes
{
    public interface IGameScene
    {
        void Render();
        bool IsSceneActive { get; set; }
    }
}
