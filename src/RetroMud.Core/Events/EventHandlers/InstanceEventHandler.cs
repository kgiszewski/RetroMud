using RetroMud.Core.Context;
using RetroMud.Core.Events.Helpers;

namespace RetroMud.Core.Events.EventHandlers
{
    public class InstanceEventHandler : IRegisterEvents
    {
        public void Register()
        {
            InstanceContext.Instance.OnInstanceStart += Instance_OnInstanceStart;
        }

        private void Instance_OnInstanceStart(object sender, System.EventArgs e)
        {

        }
    }
}
