using System;
using System.Configuration;
using RetroMud.Core.Collision;
using RetroMud.Core.Collision.Detectors;
using RetroMud.Core.Config;
using RetroMud.Core.Context;
using RetroMud.Core.Maps;
using RetroMud.Core.Maps.Coordinates;
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

                if (input.KeyChar == _moveLeftKey && player.Position.Column > 0 &&
                    _collisionDetector.CanMoveToPosition(map, new MapCoordinate(player.Position.Row, player.Position.Column - 1)))
                {
                    player.Position.Column--;
                }
                else if (input.KeyChar == _moveUpKey && player.Position.Row > 0 &&
                    _collisionDetector.CanMoveToPosition(map, new MapCoordinate(player.Position.Row - 1, player.Position.Column)))
                {
                    player.Position.Row--;
                }
                else if (input.KeyChar == _moveRightKey && player.Position.Column < map.Width - 1 &&
                         _collisionDetector.CanMoveToPosition(map, new MapCoordinate(player.Position.Row, player.Position.Column + 1)))
                {
                    player.Position.Column++;
                }
                else if (input.KeyChar == _moveDownKey && player.Position.Row < map.Height - 1 &&
                         _collisionDetector.CanMoveToPosition(map, new MapCoordinate(player.Position.Row + 1, player.Position.Column)))
                {
                    player.Position.Row++;
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

