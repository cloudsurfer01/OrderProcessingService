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