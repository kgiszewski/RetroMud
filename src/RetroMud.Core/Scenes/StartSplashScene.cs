using System;
using RetroMud.Core.Context;
using RetroMud.Core.Scenes.Helpers;

namespace RetroMud.Core.Scenes
{
    public class StartSplashScene : IGameScene
    {
        public void Render()
        {
            while (IsSceneActive)
            {
                Console.SetCursorPosition(0, 0);
                Console.CursorVisible = false;
                Console.WriteLine();

                LogoHelper.RenderLogo();

                Console.WriteLine();
                Console.WriteLine($"Version 0.0.1");

                Console.WriteLine();
                Console.WriteLine($"Copyright Kevin Giszewski 2017");
                Console.WriteLine();

                Console.WriteLine("Press any key to continue...");

                if (Console.KeyAvailable)
                {
                    ClientContext.Instance.GameSceneManager.ChangeToNextScene(new ExploreMapScene(1));
                }
            }
        }

        public bool IsSceneActive { get; set; }
    }
}
