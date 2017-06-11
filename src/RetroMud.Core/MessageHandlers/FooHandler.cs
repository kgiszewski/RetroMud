using RetroMud.Core.Context;
using RetroMud.Messaging.Dispatching;
using RetroMud.Messaging.Messages;

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
