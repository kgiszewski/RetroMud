using System.Collections.Generic;
using RetroMud.Core.Players;

namespace RetroMud.Core.Status
{
    public interface IStatusMessageManager
    {
        IEnumerable<IStatusMessage> GetMessages(int count);
        void AddStatusMessage(string message);
    }
}
