using System;
using System.Collections.Generic;

namespace Frontend_ServiceDesk.ApplicationData;

public partial class Admin
{
    public int AdminId { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<AdminsRequest> AdminsRequests { get; set; } = new List<AdminsRequest>();

    public virtual ICollection<Chat> Chats { get; set; } = new List<Chat>();

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
}
