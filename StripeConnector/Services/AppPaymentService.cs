using AutoMapper;
using Stripe;
using StripeConnector.Commands;
using StripeConnector.Models;

namespace StripeConnector.Services;

public interface IAppPaymentService
{
    Task<StripePayment?> AddStripePaymentAsync(AddPayment payment, CancellationToken ct);
}

public class AppPaymentService : IAppPaymentService
{
    private readonly ChargeService _chargeService;
    private readonly IMapper _mapper;

    public AppPaymentService(ChargeService chargeService, IMapper mapper)
    {
        _chargeService = chargeService;
        _mapper = mapper;
    }

    public async Task<StripePayment?> AddStripePaymentAsync(AddPayment payment, CancellationToken ct)
    {
        try
        {
            // Create the payment options
            var paymentOptions = _mapper.Map<ChargeCreateOptions>(payment);

            // Create the payment
            Charge createdPayment = await _chargeService.CreateAsync(paymentOptions, null, ct);

            // Return the payment
            return _mapper.Map<StripePayment>(createdPayment);
        }
        catch(Exception ex)
        {
            Console.WriteLine("Could not create the payment. Check that the provided credentials are valid "
                + $"and the customer for which the payment is done exists. Additional info: {ex.Message}");

            return null;
        }
    }
}
