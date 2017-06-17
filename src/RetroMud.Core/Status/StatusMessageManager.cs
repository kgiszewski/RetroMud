using System;
using System.Collections.Generic;
using System.Linq;
using RetroMud.Core.Status.Messages;
using RetroMud.Messaging.Publishing;

namespace RetroMud.Core.Status
{
    public class StatusMessageManager : IStatusMessageManager
    {
        private readonly ISendTcpMessages _tcpMessenger;

        public StatusMessageManager()
            : this(new TcpMessenger())
        {
            
        }

        public StatusMessageManager(ISendTcpMessages tcpMessenger)
        {
            _tcpMessenger = tcpMessenger;
        }

        public IEnumerable<IStatusMessage> GetMessages(int count)
        {
            var messages = ((GetStatusMessagesResponse)_tcpMessenger.Send(new GetStatusMessagesRequest())).StatusMessages;

            return messages.OrderBy(x => x.CreatedOn).Select(x => x);
        }
    }
}
