using Domain.Entities;
using Application.DTOs;

namespace Application.Interfaces;

public interface IOrderResponseMapper
{
    OrderResponse Map(Order order, IEnumerable<ProductItem> products);
}
