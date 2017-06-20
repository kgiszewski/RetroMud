using System;
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
            }
        }

        public bool IsSceneActive { get; set; }
    }
}
