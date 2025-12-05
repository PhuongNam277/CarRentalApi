using System;
using System.Collections.Generic;

namespace NewCarRental.Domain.Entities;

public partial class ChatMessage
{
    public long ChatMessageId { get; set; }

    public int ConversationId { get; set; }

    public int SenderId { get; set; }

    public string Content { get; set; } = null!;

    public DateTime SentAt { get; set; }

    public bool IsRead { get; set; }

    public virtual Conversation Conversation { get; set; } = null!;

    public virtual User Sender { get; set; } = null!;
}
