using Application.DTOs;
using MediatR;

namespace Application.Orders.Queries;

public class GetOrderByOrderNumberQuery(string orderNumber) : IRequest<OrderResponse>
{
    public string OrderNumber { get; } = orderNumber;
}