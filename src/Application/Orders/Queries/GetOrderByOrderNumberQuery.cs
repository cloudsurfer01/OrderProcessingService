using Application.DTOs;
using MediatR;

namespace Application.Orders.Queries;

public class GetOrderByOrderNumberQuery(Guid orderNumber) : IRequest<OrderResponse>
{
    public Guid OrderNumber { get; } = orderNumber;
}