namespace StripeConnector.Commands;

public class AddPayment
{
    public string CustomerId { get; set; } = null!;
    public string ReceiptEmail { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Currency { get; set; } = null!;
    public long Amount { get; set; }
}