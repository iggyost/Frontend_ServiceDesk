using System;
using System.Collections.Generic;

namespace Frontend_ServiceDesk.ApplicationData;

public partial class ChatsMessage
{
    public int ChatMessageId { get; set; }

    public int ChatId { get; set; }

    public int MessageId { get; set; }

    public virtual Chat Chat { get; set; } = null!;

    public virtual Message Message { get; set; } = null!;
}
