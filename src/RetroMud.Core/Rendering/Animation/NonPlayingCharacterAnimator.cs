using System.Collections.Generic;
using RetroMud.Core.Maps;
using RetroMud.Core.Maps.Coordinates;

namespace RetroMud.Core.Rendering.Animation
{
    public class NonPlayingCharacterAnimator : IAnimateNonPlayingCharacters
    {
        private List<NonPlayingCharacter> _npcList;

        public void Animate(IMap map, int frameNumber)
        {
            if(_npcList == null)
            {
                _npcList = new List<NonPlayingCharacter>();

                for (var row = 0; row < map.Buffer.Length; row++)
                {
                    var rowCharacters = map.Buffer[row].ToCharArray();

                    for (var column = 0; column < rowCharacters.Length; column++)
                    {
                        //TODO: make a list of chars
                        if (rowCharacters[column] == '&')
                        {
                            _npcList.Add(new NonPlayingCharacter
                            {
                                Position = new MapCoordinate(row, column),
                                AnimationStrategy = new SideToSideMovementStrategy(),
                                Character = '&'
                            });
                        }
                    }
                }
            }

            if (frameNumber % 5 == 0)
            {
                foreach (var npc in _npcList)
                {
                    var newPosition = npc.AnimationStrategy.GetNewPosition(map, npc.Position);
                    map.UpdateAtPosition(npc.Position, ' ');
                    map.UpdateAtPosition(newPosition, npc.Character);
                    npc.Position = newPosition;
                }
            }
        }
    }

    public class NonPlayingCharacter
    {
        public char Character { get; set; }
        public IMapCoordinate Position { get; set; }
        public IAnimationStrategy AnimationStrategy { get; set; }
    }
}
