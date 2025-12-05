using System;
using System.Collections.Generic;

namespace NewCarRental.Domain.Entities;

public partial class User
{
    public int UserId { get; set; }

    public string FullName { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? RoleId { get; set; }

    public bool IsBlocked { get; set; }

    public virtual ICollection<Blog> Blogs { get; set; } = new List<Blog>();

    public virtual ICollection<ChatMessage> ChatMessages { get; set; } = new List<ChatMessage>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Conversation> ConversationCustomers { get; set; } = new List<Conversation>();

    public virtual ICollection<ConversationReadState> ConversationReadStates { get; set; } = new List<ConversationReadState>();

    public virtual ICollection<Conversation> ConversationStaffs { get; set; } = new List<Conversation>();

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual Role? Role { get; set; }

    public virtual ICollection<TenantMembership> TenantMemberships { get; set; } = new List<TenantMembership>();

    public virtual ICollection<UserBranch> UserBranches { get; set; } = new List<UserBranch>();

    public virtual ICollection<UserConversationVisibility> UserConversationVisibilities { get; set; } = new List<UserConversationVisibility>();
}
