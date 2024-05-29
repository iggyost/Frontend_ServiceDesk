using System;
using System.Collections.Generic;

namespace Frontend_ServiceDesk.ApplicationData;

public partial class ChatsView
{
    public int ChatId { get; set; }

    public string AdminEmail { get; set; } = null!;

    public int AdminId { get; set; }

    public string UserEmail { get; set; } = null!;

    public int UserId { get; set; }
}
