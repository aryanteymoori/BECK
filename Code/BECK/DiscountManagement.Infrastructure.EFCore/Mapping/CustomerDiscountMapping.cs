using DiscountManagement.Domain.CustomerDiscountAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiscountManagement.Infrastructure.EFCore.Mapping
{
    public class CustomerDiscountMapping : IEntityTypeConfiguration<CustomerDiscount>
    {
        public void Configure(EntityTypeBuilder<CustomerDiscount> builder)
        {
            builder.ToTable("CustomerDiscounts");
            builder.HasKey(x=>x.Id);
            builder.Property(x=>x.Reason).HasMaxLength(255);
            builder.Property(x=>x.ProductId).IsRequired();
            builder.Property(x=>x.DiscountRate).IsRequired();
        }
    }
}
