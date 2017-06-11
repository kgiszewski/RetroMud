using RetroMud.Core.Config;

namespace RetroMud.Core.Context
{
    public interface IInstanceContext
    {
        string Name { get; }
        void Starting();
        IInstanceConfiguration Configuration { get; set; }
    }
}
