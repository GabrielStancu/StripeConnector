namespace StripeConnector.Commands;

public class AddCustomer
{
    public string Email { get; set; } = null!;
    public string Name { get; set; } = null!;
    public AddCard CreditCard { get; set; } = null!;
}