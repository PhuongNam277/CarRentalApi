using System;
using System.Collections.Generic;

namespace NewCarRental.Domain.Entities;

public partial class TenantCategory
{
    public int TenantId { get; set; }

    public int CategoryId { get; set; }

    public string? DisplayNameOverride { get; set; }

    public bool IsHidden { get; set; }

    public int? SortOrderOverride { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Tenant Tenant { get; set; } = null!;
}
