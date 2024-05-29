using System;
using System.Collections.Generic;

namespace Frontend_ServiceDesk.ApplicationData;

public partial class AdminsRequest
{
    public int AdminRequestId { get; set; }

    public int AdminId { get; set; }

    public int RequestId { get; set; }

    public DateTime AcceptedDate { get; set; }

    public TimeSpan AcceptedTime { get; set; }

    public DateTime? LastChangeDate { get; set; }

    public TimeSpan? LastChangeTime { get; set; }

    public virtual Admin Admin { get; set; } = null!;

    public virtual Request Request { get; set; } = null!;
}
