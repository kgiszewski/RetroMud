using RetroMud.Core.Maps;

namespace RetroMud.Core.NonPlayingCharacters.Animation
{
    public interface IAnimateNonPlayingCharacters
    {
        void Animate(IMap map, int frameNumber);
    }
}
