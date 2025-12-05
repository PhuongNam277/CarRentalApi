

namespace NewCarRental.Domain.Entities;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int ReservationId { get; set; }

    public DateTime? PaymentDate { get; set; }

    public decimal Amount { get; set; }

    public string? PaymentMethod { get; set; }

    public string? Status { get; set; }

    public int TenantId { get; set; }

    public string? VnpTransactionId { get; set; }

    public virtual Reservation Reservation { get; set; } = null!;

    public virtual Tenant Tenant { get; set; } = null!;
}
