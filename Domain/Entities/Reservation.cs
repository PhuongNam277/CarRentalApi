using System;
using System.Collections.Generic;

namespace NewCarRental.Domain.Entities;

public partial class Reservation
{
    public int ReservationId { get; set; }

    public int UserId { get; set; }

    public int CarId { get; set; }

    public DateTime? ReservationDate { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public decimal TotalPrice { get; set; }

    public string? Status { get; set; }

    public string FromCity { get; set; } = null!;

    public string ToCity { get; set; } = null!;

    public int? PickupLocationId { get; set; }

    public int? DropoffLocationId { get; set; }

    public int TenantId { get; set; }

    public virtual Car Car { get; set; } = null!;

    public virtual Location? DropoffLocation { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Location? PickupLocation { get; set; }

    public virtual Tenant Tenant { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
