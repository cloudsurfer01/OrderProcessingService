using Application.DTOs;
using Application.Orders.Commands;
using Application.Orders.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("[controller]")]
[ApiController]
public class OrderController(IMediator mediator) : Controller
{
    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
    {
        try
        {
            var command = new CreateOrderCommand(request);
            var orderNumber = await mediator.Send(command);
            return CreatedAtAction(nameof(GetOrderByNumber), new { orderNumber }, new { orderNumber });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpGet("{orderNumber}")]
    public async Task<IActionResult> GetOrderByNumber(Guid orderNumber)
    {
        var command = new GetOrderByOrderNumberQuery(orderNumber);
        var response = await mediator.Send(command);

        if (response == null)
            return NotFound();

        return Ok(response);
    }
}
