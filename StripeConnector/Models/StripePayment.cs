namespace StripeConnector.Models;

public class StripePayment
{
    public string CustomerId { get; set; } = null!;
    public string ReceiptEmail { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Currency { get; set; } = null!;
    public long Amount { get; set; }
    public string PaymentId { get; set; } = null!;
}