using AutoMapper;
using Stripe;
using StripeConnector.Commands;
using StripeConnector.Models;

namespace StripeConnector.Services;

public interface IStripeAppService
{
    Task<StripeCustomer> AddStripeCustomerAsync(AddCustomer customer, CancellationToken ct);
    Task<StripePayment> AddStripePaymentAsync(AddPayment payment, CancellationToken ct);
}

public class StripeAppService : IStripeAppService
{
    private readonly ChargeService _chargeService;
    private readonly CustomerService _customerService;
    private readonly TokenService _tokenService;
    private readonly IMapper _mapper;

    public StripeAppService(
        ChargeService chargeService,
        CustomerService customerService,
        TokenService tokenService,
        IMapper mapper)
    {
        _chargeService = chargeService;
        _customerService = customerService;
        _tokenService = tokenService;
        _mapper = mapper;
    }

    public async Task<StripeCustomer> AddStripeCustomerAsync(AddCustomer customer, CancellationToken ct)
    {
        // Create the token options with the customer's data
        var tokenOptions = new TokenCreateOptions
        {
            Card = _mapper.Map<TokenCardOptions>(customer)
        };

        // Create the Stripe token
        Token stripeToken = await _tokenService.CreateAsync(tokenOptions, null, ct);

        // Create the customer options
        var customerOptions = new CustomerCreateOptions
        {
            Name = customer.Name,
            Email = customer.Email,
            Source = stripeToken.Id
        };

        // Create customer in Stripe
        Customer createdCustomer = await _customerService.CreateAsync(customerOptions, null, ct);

        // Return the created customer
        return _mapper.Map<StripeCustomer>(createdCustomer);
    }

    public async Task<StripePayment> AddStripePaymentAsync(AddPayment payment, CancellationToken ct)
    {
        // Create the payment options
        var paymentOptions = _mapper.Map<ChargeCreateOptions>(payment);

        // Create the payment
        Charge createdPayment = await _chargeService.CreateAsync(paymentOptions, null, ct);

        // Return the payment
        return _mapper.Map<StripePayment>(createdPayment);
    }
}
