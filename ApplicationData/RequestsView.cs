using System;
using System.Collections.Generic;

namespace Frontend_ServiceDesk.ApplicationData;

public partial class RequestsView
{
    public int RequestId { get; set; }

    public string Name { get; set; } = null!;

    public string Status { get; set; } = null!;

    public int StatusId { get; set; }

    public string Category { get; set; } = null!;

    public int CategoryId { get; set; }

    public string UserEmail { get; set; } = null!;

    public int UserId { get; set; }

    public string Description { get; set; } = null!;

    public DateTime Date { get; set; }

    public TimeSpan Time { get; set; }
}
