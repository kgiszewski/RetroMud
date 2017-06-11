using System;

namespace RetroMud.Core.Events.Instance
{
    public delegate void InstanceStartHandler(object sender, EventArgs e);
    public delegate void InstanceStopHandler(object sender, EventArgs e);

    public interface IInstanceContextEvents
    {
        event InstanceStartHandler OnInstanceStart;
        event InstanceStopHandler OnInstanceStop;
    }
}
