using System;

namespace RetroMud.Core.Context
{
    public delegate void InstanceStartingHandler(object sender, EventArgs e);

    public interface IInstanceContextEvents
    {
        event InstanceStartingHandler OnInstanceStarting;
    }
}
