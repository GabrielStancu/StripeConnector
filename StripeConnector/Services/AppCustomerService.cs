using AutoMapper;
using Stripe;
using StripeConnector.Commands;

namespace StripeConnector.Services;

public interface IAppCustomerService
{
    Task<StripeCustomer?> AddStripeCustomerAsync(AddCustomer customer, CancellationToken ct);
}

public class AppCustomerService : IAppCustomerService
{
    private readonly CustomerService _customerService;
    private readonly TokenService _tokenService;
    private readonly IMapper _mapper;

    public AppCustomerService(
        CustomerService customerService,
        TokenService tokenService,
        IMapper mapper)
    {
        _customerService = customerService;
        _tokenService = tokenService;
        _mapper = mapper;
    }

    public async Task<StripeCustomer?> AddStripeCustomerAsync(AddCustomer customer, CancellationToken ct)
    {
        try
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
        catch (Exception ex)
        {
            Console.WriteLine("Could not create the customer. Check that the provided credentials are valid."
                + $"Additional info: {ex.Message}");

            return null;
        }
    }
}
