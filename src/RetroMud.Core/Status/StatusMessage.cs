using System;

namespace RetroMud.Core.Status
{
    public class StatusMessage : IStatusMessage
    {
        public string Message { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
