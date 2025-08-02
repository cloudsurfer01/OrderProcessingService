using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> orderBuilder)
    {
        orderBuilder.HasKey(o => o.OrderNumber);

        orderBuilder.Property(o => o.InvoiceAddress).IsRequired();
        orderBuilder.Property(o => o.InvoiceEmailAddress).IsRequired();
        orderBuilder.Property(o => o.CreditCardNumber).IsRequired();
        orderBuilder.Property(o => o.CreatedAt).IsRequired();

        orderBuilder.OwnsMany(o => o.Products, productBuilder =>
        {
            productBuilder.WithOwner().HasForeignKey("OrderNumber");
            productBuilder.Property(p => p.ProductId).IsRequired();
            productBuilder.Property(p => p.ProductName).IsRequired().HasMaxLength(100);
            productBuilder.Property(p => p.ProductPrice).IsRequired().HasColumnType("decimal(18,2)");
            productBuilder.Property(p => p.ProductAmount).IsRequired();
            productBuilder.ToTable("OrderProducts");
        });
    }
}
