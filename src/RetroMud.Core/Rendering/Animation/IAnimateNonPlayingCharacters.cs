using RetroMud.Core.Maps;

namespace RetroMud.Core.Rendering.Animation
{
    public interface IAnimateNonPlayingCharacters
    {
        void Animate(IMap map, int frameNumber);
    }
}
