using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> orderBuilder)
    {
        orderBuilder.HasKey(o => o.OrderNumber);

        orderBuilder.OwnsOne(o => o.InvoiceAddress, a =>
        {
            a.Property(p => p.Value).HasColumnName("InvoiceAddress").IsRequired();
        });
        orderBuilder.OwnsOne(o => o.InvoiceEmailAddress, e =>
        {
            e.Property(p => p.Value).HasColumnName("InvoiceEmailAddress").IsRequired();
        });
        orderBuilder.OwnsOne(o => o.CreditCardNumber, c =>
        {
            c.Property(p => p.Value).HasColumnName("CreditCardNumber").IsRequired();
        });

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
