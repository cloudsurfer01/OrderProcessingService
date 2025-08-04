using Application.DTOs;
using Application.Interfaces;
using Application.Orders.Commands;
using Domain.Abstractions.Repositories;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.Extensions.Logging;
using Moq;

namespace Application.Tests.Orders;

public class CreateOrderCommandHandlerTests
{
    [Fact]
    public async Task Handle_Should_Create_Order_When_Valid()
    {
        // Arrange: mock all dependencies
        var mockRepo = new Mock<IOrderRepository>();
        var mockStockChecker = new Mock<IProductStockChecker>();
        var mockProductRepo = new Mock<IProductRepository>();
        var mockFactory = new Mock<IOrderFactory>();
        var mockStockService = new Mock<IStockService>();
        var mockMapper = new Mock<IOrderResponseMapper>();
        var mockReducer = new Mock<IStockReducer>();
        var mockLogger = new Mock<ILogger<CreateOrderCommandHandler>>();

        // Sample input DTO
        var request = new CreateOrderRequest
        {
            Products =
            [
                new ProductItem { ProductId = "1", ProductName = "Laptop", ProductAmount = 1, ProductPrice = 1000 }
            ],
            InvoiceAddress = "Test Street 1",
            InvoiceEmailAddress = "test@example.com",
            InvoiceCreditCardNumber = "4111-1111-1111-1111"
        };

        // Command and expected output
        var command = new CreateOrderCommand(request);
        var expectedOrder = new Order(
            new List<Product> { new Product("1", "Laptop", 1, 1000) },
            new Address("Test Street 1"),
            new Email("test@example.com"),
            new CreditCardNumber("4111-1111-1111-1111")
        );

        var expectedResponse = new OrderResponse { OrderNumber = expectedOrder.OrderNumber.ToString() };

        // Set up mocks
        mockFactory.Setup(f => f.Create(It.IsAny<CreateOrderRequest>())).Returns(expectedOrder);
        mockMapper
            .Setup(m => m.Map(expectedOrder, It.IsAny<IEnumerable<ProductItem>>()))
            .Returns(expectedResponse);

        var handler = new CreateOrderCommandHandler(
            mockRepo.Object,
            mockStockChecker.Object,
            mockProductRepo.Object,
            mockFactory.Object,
            mockStockService.Object,
            mockMapper.Object,
            mockReducer.Object,
            mockLogger.Object
        );

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        mockRepo.Verify(r =>
                r.AddAsync(It.Is<Order>(o => o.OrderNumber == expectedOrder.OrderNumber), It.IsAny<CancellationToken>()),
            Times.Once);
        Assert.Equal(expectedOrder.OrderNumber.ToString(), result.OrderNumber);
    }
}