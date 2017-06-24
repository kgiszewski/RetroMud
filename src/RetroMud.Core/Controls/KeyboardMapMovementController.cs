using System;
using System.Configuration;
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
        private readonly ConsoleKey _moveUpKey = (ConsoleKey)Convert.ToInt32(ConfigurationManager.AppSettings[ConfigConstants.MapMoveUpKey]);
        private readonly ConsoleKey _moveDownKey = (ConsoleKey)Convert.ToInt32(ConfigurationManager.AppSettings[ConfigConstants.MapMoveDownKey]);
        private readonly ConsoleKey _moveLeftKey = (ConsoleKey)Convert.ToInt32(ConfigurationManager.AppSettings[ConfigConstants.MapMoveLeftKey]);
        private readonly ConsoleKey _moveRightKey = (ConsoleKey)Convert.ToInt32(ConfigurationManager.AppSettings[ConfigConstants.MapMoveRightKey]);
        private readonly ConsoleKey _attackKey = (ConsoleKey)Convert.ToInt32(ConfigurationManager.AppSettings[ConfigConstants.MapAttackKey]);

        private readonly ConsoleKey _inventoryKey = (ConsoleKey) Convert.ToInt32(ConfigurationManager.AppSettings[ConfigConstants.MapInventoryKey]);

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

                if (input.Key == _moveLeftKey && player.Position.Column > 0 &&
                    _collisionDetector.CanMoveToPosition(map, new MapCoordinate(player.Position.Row, player.Position.Column - 1)))
                {
                    player.Position.Column--;
                    player.Facing = Direction.West;
                }
                else if (input.Key == _moveUpKey && player.Position.Row > 0 &&
                    _collisionDetector.CanMoveToPosition(map, new MapCoordinate(player.Position.Row - 1, player.Position.Column)))
                {
                    player.Position.Row--;
                    player.Facing = Direction.North;
                }
                else if (input.Key == _moveRightKey && player.Position.Column < map.Width - 1 &&
                         _collisionDetector.CanMoveToPosition(map, new MapCoordinate(player.Position.Row, player.Position.Column + 1)))
                {
                    player.Position.Column++;
                    player.Facing = Direction.East;
                }
                else if (input.Key == _moveDownKey && player.Position.Row < map.Height - 1 &&
                         _collisionDetector.CanMoveToPosition(map, new MapCoordinate(player.Position.Row + 1, player.Position.Column)))
                {
                    player.Position.Row++;
                    player.Facing = Direction.South;
                }
                else if (input.Key == _inventoryKey)
                {
                    ClientContext.Instance.GameSceneManager.OpenModalScene(new InventoryScene());
                }
                else if (input.Key == ConsoleKey.Escape)
                {
                    ClientContext.Instance.GameSceneManager.OpenModalScene(new OptionsScene());
                }
                else if (input.Key == _attackKey)
                {
                    player.IsAttacking = true;
                    player.BeginAttackFrame = ClientContext.Instance.GameTickManager.GetFrameNumber();
                }
            }
        }
    }
}

