using System;
using RetroMud.Core.Context;
using RetroMud.Core.Scenes.Helpers;

namespace RetroMud.Core.Scenes
{
    public class OptionsScene : IGameScene
    {
        public void Setup()
        {
            Console.Clear();
        }

        public void Render()
        {
            while (IsSceneActive)
            {
                Console.SetCursorPosition(0, 0);
                Console.CursorVisible = false;
                Console.WriteLine();

                LogoHelper.RenderLogo();

                Console.WriteLine();

                Console.WriteLine("  » Save");
                Console.WriteLine("   Quit");

                if (Console.KeyAvailable)
                {
                    var input = Console.ReadKey(true);

                    if (input.Key == ConsoleKey.Escape)
                    {
                        ClientContext.Instance.GameSceneManager.CloseModalScene();
                    }
                }
            }
        }

        public bool IsSceneActive { get; set; }
    }
}
