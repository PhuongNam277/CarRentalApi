using System;
using System.Collections.Generic;

namespace NewCarRental.Domain.Entities;

public partial class Tenant
{
    public int TenantId { get; set; }

    public string Name { get; set; } = null!;

    public byte Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();

    public virtual ICollection<Conversation> Conversations { get; set; } = new List<Conversation>();

    public virtual ICollection<Location> Locations { get; set; } = new List<Location>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<TenantCategory> TenantCategories { get; set; } = new List<TenantCategory>();

    public virtual ICollection<TenantMembership> TenantMemberships { get; set; } = new List<TenantMembership>();

    public virtual ICollection<UserBranch> UserBranches { get; set; } = new List<UserBranch>();
}
