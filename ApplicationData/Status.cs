using System;
using System.Collections.Generic;

namespace Frontend_ServiceDesk.ApplicationData;

public partial class Status
{
    public int StatusId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();
}
