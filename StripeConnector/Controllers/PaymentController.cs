using Microsoft.AspNetCore.Mvc;
using StripeConnector.Commands;
using StripeConnector.Models;
using StripeConnector.Services;

namespace StripeConnector.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase
{
    private readonly IAppPaymentService _appPaymentService;

    public PaymentController(IAppPaymentService appPaymentService)
    {
        _appPaymentService = appPaymentService;
    }

    [HttpPost]
    public async Task<ActionResult<StripePayment>> AddStripePayment([FromBody] AddPayment payment, CancellationToken ct)
    {
        StripePayment? createdPayment = await _appPaymentService.AddStripePaymentAsync(payment, ct);

        if (createdPayment is null)
            return BadRequest();

        return Ok(createdPayment);
    }
}