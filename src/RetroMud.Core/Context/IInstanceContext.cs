using System.Collections.Generic;
using RetroMud.Core.Config;
using RetroMud.Core.Status;

namespace RetroMud.Core.Context
{
    public interface IInstanceContext
    {
        string Name { get; }
        void Start();
        void Stop();
        IInstanceConfiguration Configuration { get; set; }
        List<IStatusMessage> StatusMessages { get; set; }
    }
}
