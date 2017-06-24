using System;
using RetroMud.Core.Context;

namespace RetroMud
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadKey(true);
        
            Setup.Initialize();

            while (ClientContext.Instance.GameSceneManager.CurrentGameScene != null)
            {
                ClientContext.Instance.GameSceneManager.CurrentGameScene.Setup();
                ClientContext.Instance.GameSceneManager.CurrentGameScene.Render();
            }

            Console.WriteLine("Game over!");

            Console.ReadKey();
        }
    }
}
