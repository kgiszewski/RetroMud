using System;
using System.Collections.Generic;

namespace RetroMud.Core.Status
{
    public class StatusMessageManager : IStatusMessageManager
    {
        public IEnumerable<IStatusMessage> GetMessages()
        {
            return new List<IStatusMessage>
            {
                new StatusMessage
                {
                    Message = "This is the first message and it should wrap!",
                    CreatedOn = DateTime.Now
                },
                new StatusMessage
                {
                    Message = "This is the second message and it should wrap!",
                    CreatedOn = DateTime.Now.AddMinutes(-1)
                }
            };
        }
    }
}
