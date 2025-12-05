using System;
using System.Collections.Generic;

namespace NewCarRental.Domain.Entities;

public partial class Location
{
    public int LocationId { get; set; }

    public string Name { get; set; } = null!;

    public string? Address { get; set; }

    public string? City { get; set; }

    public decimal? Lat { get; set; }

    public decimal? Lng { get; set; }

    public string? TimeZone { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public int TenantId { get; set; }

    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();

    public virtual ICollection<Reservation> ReservationDropoffLocations { get; set; } = new List<Reservation>();

    public virtual ICollection<Reservation> ReservationPickupLocations { get; set; } = new List<Reservation>();

    public virtual Tenant Tenant { get; set; } = null!;

    public virtual ICollection<UserBranch> UserBranches { get; set; } = new List<UserBranch>();
}
