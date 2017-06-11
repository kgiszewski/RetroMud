using RetroMud.Core.Config;

namespace RetroMud.Core.Context
{
    public interface IInstanceContext
    {
        string Name { get; }
        void Start();
        IInstanceConfiguration Configuration { get; set; }
    }
}
