using System;
using System.Collections.Generic;

namespace Frontend_ServiceDesk.ApplicationData;

public partial class Message
{
    public int MessageId { get; set; }

    public string Message1 { get; set; } = null!;

    public string? Attachment { get; set; }

    public int? UserId { get; set; }

    public int? AdminId { get; set; }

    public virtual Admin? Admin { get; set; }

    public virtual ICollection<ChatsMessage> ChatsMessages { get; set; } = new List<ChatsMessage>();

    public virtual User? User { get; set; }
}
