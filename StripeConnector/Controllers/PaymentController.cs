using Microsoft.AspNetCore.Mvc;
using StripeConnector.Commands;
using StripeConnector.Models;
using StripeConnector.Services;

namespace StripeConnector.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase
{
    private readonly IStripeAppService _stripeService;

    public PaymentController(IStripeAppService stripeService)
    {
        _stripeService = stripeService;
    }

    public async Task<ActionResult<StripePayment>> AddStripePayment([FromBody] AddPayment payment, CancellationToken ct)
    {
        StripePayment createdPayment = await _stripeService.AddStripePaymentAsync(payment, ct);

        return StatusCode(StatusCodes.Status200OK, createdPayment);
    }
}