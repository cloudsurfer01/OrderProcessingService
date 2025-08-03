using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces;

public interface IOrderFactory
{
    Order Create(CreateOrderRequest createOrderRequest);
}
