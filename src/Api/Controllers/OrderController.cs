using Application.DTOs;
using Application.Orders.Commands;
using Application.Orders.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class OrderController(IMediator mediator) : Controller
{
    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
    {
        var command = new CreateOrderCommand(request);
        var orderNumber = await mediator.Send(command);
        return CreatedAtAction(nameof(GetOrderByNumber), new { orderNumber }, new { orderNumber });
    }

    [HttpGet("{orderNumber}")]
    public async Task<IActionResult> GetOrderByNumber(string orderNumber)
    {
        var command = new GetOrderByOrderNumberQuery(orderNumber);
        var response = await mediator.Send(command);
        return Ok(response);
    }
}
