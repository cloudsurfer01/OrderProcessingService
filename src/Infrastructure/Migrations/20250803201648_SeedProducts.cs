using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    AvailableQuantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AvailableQuantity", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), 10, "Gaming Laptop", 1499.99m },
                    { new Guid("00000000-0000-0000-0000-000000000002"), 100, "Wireless Mouse", 49.99m },
                    { new Guid("00000000-0000-0000-0000-000000000003"), 50, "Mechanical Keyboard", 129.99m },
                    { new Guid("00000000-0000-0000-0000-000000000004"), 20, "27\" Monitor", 299.99m },
                    { new Guid("00000000-0000-0000-0000-000000000005"), 75, "USB-C Hub", 39.99m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
