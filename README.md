# Order Processing Microservice

## Overview

This project is a .NET 9 microservice for order processing, built using Domain-Driven Design (DDD). It exposes two REST API endpoints for creating and retrieving orders. The solution includes proper validation, logging, and unit/integration tests across all layers.

---

## ✨Implemented Features


- Domain-Driven Design (DDD) architecture
- Order Aggregate and Value Objects in Domain layer
- MediatR for CQRS (commands/queries)
- FluentValidation for request validation
- Logging with Serilog (writes to file)
- POST /orders endpoint to create orders
- GET /orders/{orderNumber} endpoint to retrieve orders
- Swagger UI enabled for API testing
- Unit tests for Domain, Application, and Infrastructure layers
- Integration tests for creating and retrieving orders

---

## 🧰 Technologies 

- **Framework**: .NET 9 (ASP.NET Core Web API)
- **Architecture**: Domain-Driven Design (DDD)
- **Database**: PostgreSQL or SQL Server (via local or Docker)
- **ORM**: Entity Framework Core
- **Communication**: REST (JSON)
- **Validation**: FluentValidation
- **Logging**: Serilog
- **Testing**: xUnit for unit and integration tests

---

## 🏛️ Architecture

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

## 🌐 API Endpoints

### 1. Create Order

**POST** `/orders`

Creates a new order.

**Request Example:**

```json
{
  "products": [
    {
      "productId": "00000000-0000-0000-0000-000000000001",
      "productName": "Gaming Laptop",
      "productAmount": 2,
      "productPrice": 1499.99
    }
  ],
  "invoiceAddress": "123 Sample Street, 90402 Berlin",
  "invoiceEmailAddress": "customer@example.com",
  "invoiceCreditCardNumber": "4111-1111-1111-1111"
}
```

**Response Example**

```json 
{
  "orderNumber": "361ae257-50f0-44a3-bc15-7928e0284bf2"
}
```

### 2. Get Order by Order Number

**GET** `/orders/{orderNumber}`

Retrieves the full order details by its order number (UUID or integer).

**Path Parameter:**
- `orderNumber` — The unique ID of the order to retrieve.

**Request Example**

```http
GET /orders/d04d5576-6b24-4179-bddb-1138665f0398
```
**Response Example**
```json
{
  "orderNumber": "d04d5576-6b24-4179-bddb-1138665f0398",
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
  "invoiceCreditCardNumber": "1234-5678-9101-1121",
  "createdAt": "2025-03-07T12:00:00Z"
}
```

**Responses:**
```
- 200 OK — Order found and returned
- 404 Not Found — No order found with the provided number
```

## 🐳🛢️Docker & Database Setup

This project includes a `docker-compose.yml` file for spinning up a PostgreSQL container.

### 🔧 How to Start PostgreSQL via Docker:

```bash
docker-compose up -d
```

## Database Tables

This service uses Entity Framework Core to manage the database schema. The following tables are created in the `OrderDb` database:

### 1. `Orders`
Stores the order-level details.

| Column Name         | Type       | Description                   |
|---------------------|------------|-------------------------------|
| `OrderNumber`       | UUID (PK)  | Unique identifier for order   |
| `InvoiceAddress`    | string     | Customer invoice address      |
| `InvoiceEmail`      | string     | Customer email address        |
| `InvoiceCreditCard` | string     | Credit card number (validated)|
| `CreatedAt`         | datetime   | UTC creation timestamp        |

### 2. `OrderItems`
Stores product items per order.

| Column Name     | Type      | Description                       |
|-----------------|-----------|-----------------------------------|
| `Id`            | UUID (PK) | Internal ID                       |
| `OrderNumber`   | UUID (FK) | Reference to parent order         |
| `ProductId`     | string    | Product SKU or code               |
| `ProductName`   | string    | Name of the product               |
| `ProductAmount` | int       | Quantity ordered                  |
| `ProductPrice`  | decimal   | Price per unit                    |

## 3. Products Table

The `Products` table stores master data for all available products that can be ordered.

| Column Name | Type    | Description                      |
|-------------|---------|----------------------------------|
| `ProductId` | string  | Unique identifier (SKU or GUID)  |
| `Name`      | string  | Product name                     |
| `Price`     | decimal | Current price per unit           |
| `Stock`     | int     | Available quantity in inventory  |

> 🛠️ Note: Product stock availability is verified using `IStockService.CheckAvailabilityAsync()` before order creation.  
> Upon successful order creation, `IStockService.ReduceAsync()` is called to update inventory levels.


This table is used by the order creation flow to:

- Validate product existence
- Check available stock before accepting the order
- Reduce stock after a successful order

> 🛠️ Note: Stock validation and reduction are handled via `IStockService`.

> 🔄 Tables are auto-created via EF Core migrations.

## 🛢️ Database Migrations

The database schema is created and managed using **EF Core design-time migrations**.

### ✅ Migration Setup

Migrations are created using the standard EF Core CLI:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

## ▶️ Running the Application

Follow these steps to run the application locally:

### 1. Clone the Repository

```bash
git clone https://github.com/your-username/order-processing-service.git
cd order-processing-service
```

Absolutely — here’s the entire **"Running the Application"** section in clean Markdown, ready to paste into your `README.md`. This version works perfectly with copy buttons in GitHub or Markdown preview tools:

---

````markdown
## ▶️ Running the Application

Follow these steps to run the application locally:

### 1. Clone the Repository

```bash
git clone https://github.com/your-username/order-processing-service.git
cd order-processing-service
````

### 2. Start PostgreSQL via Docker

Make sure Docker is installed and running. Then run:

```bash
docker-compose up -d
```

> 🐳 This starts a PostgreSQL container using the configuration in `docker-compose.yml`.

### 3. Apply EF Core Migrations (if not automatic)

```bash
dotnet ef database update
```

> 📦 The project uses EF Core design-time migrations with a `DbContextFactory`.

### 4. Run the Web API

```bash
dotnet run --project src/API
```

This will launch the ASP.NET Core Web API.

Default URL:

```
https://localhost:5001/swagger
```

You can test:

* `POST /orders` — Create an order
* `GET /orders/{orderNumber}` — Fetch order details

### 5. Run Tests

```bash
dotnet test
```

This runs:

* ✅ Unit tests (Domain, Application, Infrastructure)
* ✅ Integration tests (API endpoints)

---

## 🔧 Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download)
- [Docker Desktop](https://www.docker.com/products/docker-desktop) – for PostgreSQL container
- EF Core CLI (install via `dotnet tool install --global dotnet-ef`)
- Git (to clone the repository)

Let me know if you want to include `.env` support, launch profiles, or Postman collection setup.



