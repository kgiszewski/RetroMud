using System;
using System.Configuration;
using RetroMud.Core.Collision;
using RetroMud.Core.Config;
using RetroMud.Core.Context;
using RetroMud.Core.Maps;
using RetroMud.Core.Scenes;

namespace RetroMud.Core.Controls
{
    public class KeyboardMapMovementController : IHandleMapMovementControls
    {
        private readonly IHandleCollisionDetection _collisionDetector;
        private readonly char _moveUpKey = ConfigurationManager.AppSettings[ConfigConstants.MapMoveUpKey].ToCharArray()[0];
        private readonly char _moveDownKey = ConfigurationManager.AppSettings[ConfigConstants.MapMoveDownKey].ToCharArray()[0];
        private readonly char _moveLeftKey = ConfigurationManager.AppSettings[ConfigConstants.MapMoveLeftKey].ToCharArray()[0];
        private readonly char _moveRightKey = ConfigurationManager.AppSettings[ConfigConstants.MapMoveRightKey].ToCharArray()[0];
        private readonly char _inventoryKey = ConfigurationManager.AppSettings[ConfigConstants.MapInventoryKey].ToCharArray()[0];

        public KeyboardMapMovementController()
            :this(CollisionDetector.Instance)
        {
            
        }

        public KeyboardMapMovementController(IHandleCollisionDetection collisionDetector)
        {
            _collisionDetector = collisionDetector;
        }

        public void HandleInput(IMap map)
        {
            if (Console.KeyAvailable)
            {
                var input = Console.ReadKey(true);
                var player = ClientContext.Instance.Player;

                if (input.KeyChar == _moveLeftKey && player.CurrentColumn > 0 &&
                    _collisionDetector.CanMoveToPosition(map, player.CurrentRow, player.CurrentColumn - 1))
                {
                    player.CurrentColumn--;
                }
                else if (input.KeyChar == _moveUpKey && player.CurrentRow > 0 &&
                    _collisionDetector.CanMoveToPosition(map, player.CurrentRow - 1, player.CurrentColumn))
                {
                    player.CurrentRow--;
                }
                else if (input.KeyChar == _moveRightKey && player.CurrentColumn < map.Width - 1 &&
                         _collisionDetector.CanMoveToPosition(map, player.CurrentRow, player.CurrentColumn + 1))
                {
                    player.CurrentColumn++;
                }
                else if (input.KeyChar == _moveDownKey && player.CurrentRow < map.Height - 1 &&
                         _collisionDetector.CanMoveToPosition(map, player.CurrentRow + 1, player.CurrentColumn))
                {
                    player.CurrentRow++;
                }
                else if (input.KeyChar == _inventoryKey)
                {
                    ClientContext.Instance.GameSceneManager.OpenModalScene(new InventoryScene());
                }
                else if (input.Key == ConsoleKey.Escape)
                {
                    ClientContext.Instance.GameSceneManager.OpenModalScene(new OptionsScene());
                }
            }
        }
    }
}

