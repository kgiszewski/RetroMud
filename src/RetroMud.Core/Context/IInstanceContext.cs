using RetroMud.Core.Config;

namespace RetroMud.Core.Context
{
    public interface IInstanceContext
    {
        string Name { get; }
        void Start();
        void Stop();
        IInstanceConfiguration Configuration { get; set; }
    }
}
