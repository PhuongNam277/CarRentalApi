using System;
using System.Collections.Generic;

namespace NewCarRental.Domain.Entities;

public partial class UserBranch
{
    public int UserId { get; set; }

    public int TenantId { get; set; }

    public int LocationId { get; set; }

    public virtual Location Location { get; set; } = null!;

    public virtual Tenant Tenant { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
