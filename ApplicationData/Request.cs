using System;
using System.Collections.Generic;

namespace Frontend_ServiceDesk.ApplicationData;

public partial class Request
{
    public int RequestId { get; set; }

    public string Name { get; set; } = null!;

    public int StatusId { get; set; }

    public int CategoryId { get; set; }

    public int UserId { get; set; }

    public string Description { get; set; } = null!;

    public DateTime Date { get; set; }

    public TimeSpan Time { get; set; }

    public virtual ICollection<AdminsRequest> AdminsRequests { get; set; } = new List<AdminsRequest>();

    public virtual Category Category { get; set; } = null!;

    public virtual Status Status { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
