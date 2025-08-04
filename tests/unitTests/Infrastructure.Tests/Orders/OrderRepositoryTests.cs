using Application.Interfaces;
using Domain.Entities;
using Domain.ValueObjects;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;


public class OrderRepositoryTests
{
    private async Task<OrderDbContext> CreateInMemoryDbContextAsync()
    {
        var options = new DbContextOptionsBuilder<OrderDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        .Options;

        var context = new OrderDbContext(options);
        await context.Database.EnsureCreatedAsync();
        return context;
    }

    [Fact]
    public async Task AddAsync_Should_Save_Order_To_Database()
    {
        // Arrange
        var context = await CreateInMemoryDbContextAsync();
        var repository = new OrderRepository(context);

        var products = new List<Product>
        {
            new Product("1", "Laptop", 1, 1000m),
            new Product("2", "Mouse", 2, 25m)
        };

        var address = new Address("Test Street 123");
        var email = new Email("test@example.com");
        var creditCard = new CreditCardNumber("4111-1111-1111-1111");
        var order = new Order(products, address, email, creditCard);

        // Act
        await repository.AddAsync(order);
        await context.SaveChangesAsync();

        var savedOrder = await context.Orders.FindAsync(order.OrderNumber);

        // Assert
        Assert.NotNull(savedOrder);
        Assert.Equal(order.OrderNumber, savedOrder.OrderNumber);
        Assert.Equal(2, savedOrder.Products.Count);
        Assert.Equal("test@example.com", savedOrder.InvoiceEmailAddress.Value);
    }

    [Fact]
    public async Task GetByOrderNumberAsync_Should_Return_Order_When_Exists()
    {
        // Arrange
        var context = await CreateInMemoryDbContextAsync();
        var repository = new OrderRepository(context);

        var products = new List<Product>
        {
            new Product("1", "Keyboard", 1, 100m)
        };

        var address = new Address("Berlin Street 42");
        var email = new Email("customer@demo.com");
        var creditCard = new CreditCardNumber("4000-0000-0000-0002");

        var order = new Order(products, address, email, creditCard);

        await context.Orders.AddAsync(order);
        await context.SaveChangesAsync();

        // Act
        var fetchedOrder = await repository.GetByOrderNumberAsync(order.OrderNumber);

        // Assert
        Assert.NotNull(fetchedOrder);
        Assert.Equal(order.OrderNumber, fetchedOrder.OrderNumber);
        Assert.Equal(1, fetchedOrder.Products.Count);
        Assert.Equal("Keyboard", fetchedOrder.Products[0].ProductName);
    }

    [Fact]
    public async Task GetByOrderNumberAsync_Should_Return_Null_When_Not_Found()
    {
        // Arrange
        var context = await CreateInMemoryDbContextAsync();
        var repository = new OrderRepository(context);
        var nonExistentOrderNumber = Guid.NewGuid();

        // Act
        var result = await repository.GetByOrderNumberAsync(nonExistentOrderNumber);

        // Assert
        Assert.Null(result);
    }
}
