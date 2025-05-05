using Order = Domain.Entities.OrderModule.Order;

namespace Persistance.Data.Configurations;
internal class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");
        builder.Property(o => o.SubTotal)
            .HasColumnType("decimal(8,2)");
        builder.HasMany(o => o.items)
            .WithOne();

        builder.HasOne(o => o.DeliveryMethod)
            .WithMany()
            .HasForeignKey(o => o.DeliveryMethodId);

        builder.OwnsOne(o => o.Address, a =>
        {
            a.WithOwner();
            a.Property(a => a.Street).HasColumnName("Street");
            a.Property(a => a.City).HasColumnName("City");
            a.Property(a => a.Country).HasColumnName("Country");
        });
    }
}
