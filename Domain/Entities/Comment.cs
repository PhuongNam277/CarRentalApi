using System;
using System.Collections.Generic;

namespace NewCarRental.Domain.Entities;

public partial class Comment
{
    public int CommentId { get; set; }

    public int BlogId { get; set; }

    public string AuthorName { get; set; } = null!;

    public string Content { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public int? UserId { get; set; }

    public bool IsAnonymous { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User? User { get; set; }
}
