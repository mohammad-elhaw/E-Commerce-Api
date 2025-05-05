using Domain.Entities.OrderModule;

namespace Persistance.Data.Configurations;
internal class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItems");
        builder.OwnsOne(oi => oi.Product, p =>
        {
            p.WithOwner();
            p.Property(p => p.ProductId).HasColumnName("ProductId");
            p.Property(p => p.ProductName).HasColumnName("ProductName");
            p.Property(p => p.PictureUrl).HasColumnName("PictureUrl");
        });

        builder.Property(oi => oi.Price)
            .HasColumnType("decimal(8,2)");
    }
}
