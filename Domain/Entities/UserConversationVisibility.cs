using System;
using System.Collections.Generic;

namespace NewCarRental.Domain.Entities;

public partial class UserConversationVisibility
{
    public int UserId { get; set; }

    public int ConversationId { get; set; }

    public bool IsHidden { get; set; }

    public virtual Conversation Conversation { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
