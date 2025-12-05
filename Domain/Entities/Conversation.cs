using System;
using System.Collections.Generic;

namespace NewCarRental.Domain.Entities;

public partial class Conversation
{
    public int ConversationId { get; set; }

    public int CustomerId { get; set; }

    public int? StaffId { get; set; }

    public string Status { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime LastMessageAt { get; set; }

    public DateTime? ClosedAt { get; set; }

    public int? TenantId { get; set; }

    public virtual ICollection<ChatMessage> ChatMessages { get; set; } = new List<ChatMessage>();

    public virtual ICollection<ConversationReadState> ConversationReadStates { get; set; } = new List<ConversationReadState>();

    public virtual User Customer { get; set; } = null!;

    public virtual User? Staff { get; set; }

    public virtual Tenant? Tenant { get; set; }

    public virtual ICollection<UserConversationVisibility> UserConversationVisibilities { get; set; } = new List<UserConversationVisibility>();
}
