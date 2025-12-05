
using NewCarRental.Domain.Entities;

namespace NewCarRental.Domain.Entities;

public partial class ConversationReadState
{
    public int ConversationId { get; set; }

    public int UserId { get; set; }

    public long LastReadMessageId { get; set; }

    public virtual Conversation Conversation { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
