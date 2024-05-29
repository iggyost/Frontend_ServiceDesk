using System;
using System.Collections.Generic;

namespace Frontend_ServiceDesk.ApplicationData;

public partial class MessagesView
{
    public int MessageId { get; set; }

    public string Message { get; set; } = null!;

    public string? Attachment { get; set; }

    public int? AdminId { get; set; }

    public string AdminEmail { get; set; } = null!;

    public int? UserId { get; set; }

    public string UserEmail { get; set; } = null!;

    public int ChatId { get; set; }
}
