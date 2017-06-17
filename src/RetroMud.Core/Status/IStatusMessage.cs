using System;

namespace RetroMud.Core.Status
{
    public interface IStatusMessage
    {
        string Message { get; set; }
        DateTime CreatedOn { get; set; }
    }
}
