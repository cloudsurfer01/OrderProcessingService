using System.Net;
using System.Net.Http.Json;
using Application.DTOs;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Integration.Tests.Orders;

public class PostOrdersTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task Should_Create_Order_When_Valid_Request()
    {
        // Arrange
        var request = new CreateOrderRequest
        {
            Products =
            [
                new ProductItem
                {
                    ProductId = "00000000-0000-0000-0000-000000000002", 
                    ProductName = "Wireless Mouse", 
                    ProductAmount = 1, 
                    ProductPrice = 49.99m
                }
            ],
            InvoiceAddress = "Integration: 123 Sample Street, 90402 Berlin",
            InvoiceEmailAddress = "ikhan@paessler.com",
            InvoiceCreditCardNumber = "4111-1111-1111-1111"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/Order", request);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }
}