using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Infrastructure.Configurations;

public class ProductEntityConfiguration : IEntityTypeConfiguration<ProductEntity>
{
    public void Configure(EntityTypeBuilder<ProductEntity> builder)
    {
        builder.HasKey(p => p.Id);
        /*
        builder.HasData(
            new ProductEntity
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                Name = "Gaming Laptop",
                Price = 1499.99m,
                AvailableQuantity = 10
            },
            new ProductEntity
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                Name = "Wireless Mouse",
                Price = 49.99m,
                AvailableQuantity = 100
            },
            new ProductEntity
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                Name = "Mechanical Keyboard",
                Price = 129.99m,
                AvailableQuantity = 50
            },
            new ProductEntity
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                Name = "27\" Monitor",
                Price = 299.99m,
                AvailableQuantity = 20
            },
            new ProductEntity
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                Name = "USB-C Hub",
                Price = 39.99m,
                AvailableQuantity = 75
            }        
        );
        */
    }
}
