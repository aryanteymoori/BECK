using BlogManagement.Domain.ArticleCategoryAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogManagement.Infrastructure.EFCore.Mapping
{
    public class ArticleCategoryMapping : IEntityTypeConfiguration<ArticleCategory>
    {
        public void Configure(EntityTypeBuilder<ArticleCategory> builder)
        {
            builder.ToTable("ArticleCategories");
            builder.HasKey(a => a.Id);
            builder.Property(x=>x.Name).HasMaxLength(255).IsRequired();
            builder.Property(x=>x.Description).HasMaxLength(500);
            builder.Property(x=>x.Slug).HasMaxLength(255).IsRequired();
            builder.Property(x=>x.Keywords).HasMaxLength(150);
            builder.Property(x=>x.MetaDescription).HasMaxLength(700);
            builder.Property(x=>x.CanonicalAddress).HasMaxLength(800);

            builder.HasMany(x => x.Articles).WithOne(x => x.ArticleCategory)
                .HasForeignKey(x => x.CategoryId);
        }
    }
}
