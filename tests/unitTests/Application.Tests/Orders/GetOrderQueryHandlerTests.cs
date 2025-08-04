using Application.DTOs;
using Application.Interfaces;
using Application.Orders.Queries;
using Domain.Abstractions.Repositories;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.Extensions.Logging;
using Moq;

namespace Application.Tests.Orders;

public class GetOrderQueryHandlerTests
{
    [Fact]
    public async Task Handle_Should_Return_Order_When_Found()
    {
        // Arrange
        var mockRepo = new Mock<IOrderRepository>();
        var mockMapper = new Mock<IOrderResponseMapper>();
        var mockLogger = new Mock<ILogger<GetOrderByOrderNumberQueryHandler>>();

        var orderNumber = Guid.NewGuid();

        var order = new Order(
            new List<Product> { new Product("1", "Item", 1, 100) },
            new Address("Street"),
            new Email("customer@example.com"),
            new CreditCardNumber("4111-1111-1111-1111")
        );

        var expectedResponse = new OrderResponse { OrderNumber = order.OrderNumber.ToString() };

        mockRepo.Setup(r => r.GetByOrderNumberAsync(orderNumber, It.IsAny<CancellationToken>()))
            .ReturnsAsync(order);

        mockMapper.Setup(m => m.Map(order, It.IsAny<IEnumerable<ProductItem>>()))
            .Returns(expectedResponse);

        var handler = new GetOrderByOrderNumberQueryHandler(mockRepo.Object, mockLogger.Object);
        
        var query = new GetOrderByOrderNumberQuery(orderNumber);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedResponse.OrderNumber, result.OrderNumber);
    }

    [Fact]
    public async Task Handle_Should_Throw_When_Order_Not_Found()
    {
        // Arrange
        var mockRepo = new Mock<IOrderRepository>();
        var mockLogger = new Mock<ILogger<GetOrderByOrderNumberQueryHandler>>();

        var orderNumber = Guid.NewGuid();

        mockRepo.Setup(r => r.GetByOrderNumberAsync(orderNumber, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Order?)null);

        var handler = new GetOrderByOrderNumberQueryHandler(mockRepo.Object, mockLogger.Object);
        var query = new GetOrderByOrderNumberQuery(orderNumber);

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            handler.Handle(query, CancellationToken.None));
    }
}