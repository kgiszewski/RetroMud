using System.Collections.Generic;
using RetroMud.Core.Context;
using RetroMud.Core.Maps;
using RetroMud.Core.Maps.Helpers;

namespace RetroMud.Core.NonPlayingCharacters.Animation
{
    public class NonPlayingCharacterAnimator : IAnimateNonPlayingCharacters
    {
        private List<INonPlayingCharacter> _npcList;

        public void Animate(IMap map)
        {
            if(_npcList == null)
            {
                _npcList = MapHelper.GetNpcForMap(map);
            }

            foreach (var npc in _npcList)
            {
                var newPosition = npc.MovementStrategy.GetNewPosition(map, npc.Position);
                map.UpdateBufferAtPosition(npc.Position, ' ');
                map.UpdateBufferAtPosition(newPosition, npc.Character);
                npc.Position = newPosition;
            }
        }
    }
}
