using System.Collections.Generic;

namespace RetroMud.Core.Status
{
    public interface IStatusMessageManager
    {
        IEnumerable<IStatusMessage> GetMessages(int count);
    }
}
