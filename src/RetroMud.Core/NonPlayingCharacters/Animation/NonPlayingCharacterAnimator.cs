using System.Collections.Generic;
using RetroMud.Core.Maps;
using RetroMud.Core.Maps.Helpers;

namespace RetroMud.Core.NonPlayingCharacters.Animation
{
    public class NonPlayingCharacterAnimator : IAnimateNonPlayingCharacters
    {
        private List<INonPlayingCharacter> _npcList;

        public void Animate(IMap map, int frameNumber)
        {
            if(_npcList == null)
            {
                _npcList = MapHelper.GetNpcForMap(map);
            }

            if (frameNumber % 10 == 0)
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
}
