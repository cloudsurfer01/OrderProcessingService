using Application.DTOs;
using MediatR;

namespace Application.Orders.Commands;

public class CreateOrderCommand(CreateOrderRequest request) : IRequest<OrderResponse>
{
    public CreateOrderRequest Request { get; } = request;
}
