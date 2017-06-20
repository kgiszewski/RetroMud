using System;
using RetroMud.Core.Context;

namespace RetroMud.Core.Controls
{
    public class InventoryController : IHandleInventoryControls
    {
        public void HandleInput()
        {
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
}
