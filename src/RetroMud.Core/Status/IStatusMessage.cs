using System;

namespace RetroMud.Core.Status
{
    public interface IStatusMessage
    {
        int PlayerId { get; set; }
        string Message { get; set; }
        DateTime CreatedOn { get; set; }
    }
}
