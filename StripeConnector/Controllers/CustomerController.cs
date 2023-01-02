using Microsoft.AspNetCore.Mvc;
using StripeConnector.Commands;
using StripeConnector.Services;

namespace StripeConnector.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly IAppCustomerService _appCustomerService;

    public CustomerController(IAppCustomerService appCustomerService)
    {
        _appCustomerService = appCustomerService;
    }

    [HttpPost]
    public async Task<ActionResult<StripeCustomer>> AddStripeCustomer([FromBody] AddCustomer customer, CancellationToken ct)
    {
        StripeCustomer? createdCustomer = await _appCustomerService.AddStripeCustomerAsync(customer, ct);

        if (createdCustomer is null)
            return BadRequest();

        return Ok(createdCustomer);
    }
}