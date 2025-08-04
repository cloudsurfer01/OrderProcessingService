# Order Processing Microservice

## Overview

This project is a .NET 9 microservice for order processing, built using Domain-Driven Design (DDD). It exposes two REST API endpoints for creating and retrieving orders. The solution includes proper validation, logging, and unit/integration tests across all layers.

---

## Features

- **Order Creation**: Submit new orders with product and invoice details.
- **Order Retrieval**: Fetch order details using the order number.
- **Validation**: Email format, stock check, and input data integrity.
- **Error Handling**: Proper HTTP responses for invalid requests or issues.
- **Logging**: Structured logging using Serilog to file and/or Azure App Insights.

---

- **Framework**: .NET 9 (ASP.NET Core Web API)
- **Architecture**: Domain-Driven Design (DDD)
- **Database**: PostgreSQL or SQL Server (via local or Docker)
- **ORM**: Entity Framework Core
- **Communication**: REST (JSON)
- **Validation**: FluentValidation
- **Logging**: Serilog
- **Testing**: xUnit for unit and integration tests

---

## Architecture

This project follows Domain-Driven Design (DDD) and clean architecture principles. It is structured into four logical layers:

### 1. Domain Layer
- Contains core business logic.
- Includes:
  - `Order` Aggregate (root entity)
  - `OrderItem` as a Value Object
  - Business rules (e.g., total price, validation rules)

### 2. Application Layer
- Defines use cases using the CQRS pattern.
- Uses **MediatR** to handle:
  - `CreateOrderCommand`
  - `GetOrderByOrderNumberQuery`
- Contains DTOs and service interfaces.

### 3. Infrastructure Layer
- Implements persistence using **Entity Framework Core** with **PostgreSQL**.
- Includes:
  - EF Core `DbContext` setup
  - Repository pattern
  - Configuration and entity mappings

### 4. API Layer
- Exposes endpoints via **ASP.NET Core Web API**.
- Includes:
  - Controllers
  - Request validation using **FluentValidation**
  - Serilog-based logging
  - Swagger UI for testing

Each layer has a clear responsibility and communicates through interfaces, promoting testability and separation of concerns.

---

## ?? API Endpoints

### 1. Create Order

**POST** `/orders`

Creates a new order.

**Request Example:**

```json
{
  "products": [
    {
      "productId": "12345",
      "productName": "Gaming Laptop",
      "productAmount": 2,
      "productPrice": 1499.99
    }
  ],
  "invoiceAddress": "123 Sample Street, 90402 Berlin",
  "invoiceEmailAddress": "customer@example.com",
  "invoiceCreditCardNumber": "1234-5678-9101-1121"
}
```

**Response Example**

