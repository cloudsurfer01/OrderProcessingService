using Application.DTOs;
using MediatR;
using System;

namespace Application.Orders.Commands;

public class CreateOrderCommand(CreateOrderRequest request) : IRequest<Guid>
{
    public CreateOrderRequest Request { get; } = request;
}
