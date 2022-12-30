using Microsoft.AspNetCore.Mvc;
using StripeConnector.Commands;
using StripeConnector.Services;

namespace StripeConnector.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly IStripeAppService _stripeService;

    public CustomerController(IStripeAppService stripeService)
    {
        _stripeService = stripeService;
    }

    [HttpPost]
    public async Task<ActionResult<StripeCustomer>> AddStripeCustomer([FromBody] AddCustomer customer, CancellationToken ct)
    {
        StripeCustomer createdCustomer = await _stripeService.AddStripeCustomerAsync(customer, ct);

        return StatusCode(StatusCodes.Status200OK, createdCustomer);
    }
}