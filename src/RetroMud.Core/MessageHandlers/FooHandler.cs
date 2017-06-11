using RetroMud.Core.Context;
using RetroMud.Core.Messages;
using RetroMud.Tcp.Messaging.Dispatching;

namespace RetroMud.Core.MessageHandlers
{
    public class FooHandler : IHandleTcpMessages<FooMessage>
    {
        public object Handle(FooMessage message)
        {
            return "Responding from the handler!" + InstanceContext.Instance.Name;
        }
    }
}
