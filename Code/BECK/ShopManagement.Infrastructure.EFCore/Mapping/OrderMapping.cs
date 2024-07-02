using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.OrderAgg;

namespace ShopManagement.Infrastructure.EFCore.Mapping
{
    public class OrderMapping : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.IssueTrackingNumber).HasMaxLength(8).IsRequired();

            builder.OwnsMany(x => x.Items, builderOptions =>
            {
                builderOptions.ToTable("OrderItems");
                builderOptions.HasKey(x => x.Id);
                builderOptions.WithOwner(x => x.Order)
                    .HasForeignKey(x => x.OrderId);
            });
        }
    }
}
