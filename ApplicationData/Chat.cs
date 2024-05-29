using System;
using System.Collections.Generic;

namespace Frontend_ServiceDesk.ApplicationData;

public partial class Chat
{
    public int ChatId { get; set; }

    public int AdminId { get; set; }

    public int UserId { get; set; }

    public virtual Admin Admin { get; set; } = null!;

    public virtual ICollection<ChatsMessage> ChatsMessages { get; set; } = new List<ChatsMessage>();

    public virtual User User { get; set; } = null!;
}
