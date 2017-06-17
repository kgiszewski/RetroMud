using System.Collections.Generic;
using System.Linq;
using RetroMud.Core.Players;
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
            var messages = _tcpMessenger.Send<GetStatusMessagesResponse>(new GetStatusMessagesRequest
            {
                PlayerId = 1
            }).StatusMessages;

            return messages.OrderBy(x => x.CreatedOn).Select(x => x);
        }

        public void AddStatusMessage(IPlayer player, string message)
        {
            _tcpMessenger.Send<AddStatusMessageResponse>(new AddStatusMessageRequest
            {
                Message = message
            });
        }
    }
}
