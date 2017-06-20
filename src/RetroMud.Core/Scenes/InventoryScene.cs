using System;
using RetroMud.Core.Context;
using RetroMud.Core.Controls;

namespace RetroMud.Core.Scenes
{
    public class InventoryScene : IGameScene
    {
        private readonly IHandleInventoryControls _inventoryController;

        public InventoryScene()
            :this(new InventoryController())
        {

        }

        public InventoryScene(IHandleInventoryControls inventoryController)
        {
            _inventoryController = inventoryController;
            IsSceneActive = true;
        }

        public void Render()
        {
            Console.Clear();
            Console.WriteLine("Inventory!");
            
            while (IsSceneActive)
            {
                _inventoryController.HandleInput();

                _renderInventory();
            }
        }

        private void _renderInventory()
        {
            Console.SetCursorPosition(0, 0);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write($"Gold: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"{ClientContext.Instance.Player.Gold:00000000}");
            Console.WriteLine();
            Console.WriteLine();
            Console.ResetColor();
        }

        public bool IsSceneActive { get; set; }
    }
}
