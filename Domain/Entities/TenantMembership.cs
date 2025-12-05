using System;
using System.Collections.Generic;

namespace NewCarRental.Domain.Entities;

public partial class TenantMembership
{
    public int TenantId { get; set; }

    public int UserId { get; set; }

    public string Role { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual Tenant Tenant { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
