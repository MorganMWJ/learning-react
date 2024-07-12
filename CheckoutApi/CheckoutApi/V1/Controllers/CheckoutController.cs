using Asp.Versioning;
using CheckoutApi.Authentication;
using CheckoutApi.V1.Models;
using CheckoutApi.V1.Models.Requests;
using CheckoutApi.V1.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CheckoutApi.V1.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/[controller]")]
[Produces("application/json")]
//[EnableCors]
public class CheckoutController : ControllerBase
{
    private readonly IMediator _mediator;

    public CheckoutController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    //[Authorize(Roles = Roles.Admin)]
    [ServiceFilter(typeof(ApiKeyAuthFilter))]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CheckoutResponse>> Checkout(CheckoutRequest request)
    {
        var response = await _mediator.Send(request);

        return new AcceptedResult(default(string), response);
    }

    [HttpGet("stock")]
    [ServiceFilter(typeof(ApiKeyAuthFilter))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<StockItem>>> AllStock()
    {
        var response = await _mediator.Send(new GetStockListRequest());

        return new OkObjectResult(response);
    }

}
