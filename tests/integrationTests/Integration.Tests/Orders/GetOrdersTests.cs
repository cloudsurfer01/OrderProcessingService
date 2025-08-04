using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Application.DTOs;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Integration.Tests.Orders;

public class GetOrdersTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task Should_Return_Order_When_Exists()
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
            InvoiceAddress = "Valid Address",
            InvoiceEmailAddress = "ikhan@paessler.com",
            InvoiceCreditCardNumber = "4111-1111-1111-1111"
        };

        var createResponse = await _client.PostAsJsonAsync("/Order", request);
        createResponse.EnsureSuccessStatusCode();

        var json = await createResponse.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(json);
        var orderNumber = doc.RootElement
                             .GetProperty("orderNumber")
                             .GetProperty("orderNumber")
                             .GetString();

        // Act
        var getResponse = await _client.GetAsync($"/Order/{orderNumber}");

        // Assert
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

        var orderJson = await getResponse.Content.ReadAsStringAsync();
        using var orderDoc = JsonDocument.Parse(orderJson);
        var fetchedOrderNumber = orderDoc.RootElement.GetProperty("orderNumber").GetString();
        var productName = orderDoc.RootElement.GetProperty("products")[0].GetProperty("productName").GetString();
        var email = orderDoc.RootElement.GetProperty("invoiceEmailAddress").GetString();

        Assert.Equal(orderNumber, fetchedOrderNumber);
        Assert.Equal("Wireless Mouse", productName);
        Assert.Equal("ikhan@paessler.com", email);
    }

    [Fact]
    public async Task Should_Return_404_When_Order_Not_Found()
    {
        // Arrange
        var nonExistentOrderNumber = Guid.NewGuid();

        // Act
        var response = await _client.GetAsync($"/Order/{nonExistentOrderNumber}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}