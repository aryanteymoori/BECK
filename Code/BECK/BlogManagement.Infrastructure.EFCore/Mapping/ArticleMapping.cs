using BlogManagement.Domain.ArticleAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogManagement.Infrastructure.EFCore.Mapping
{
	public class ArticleMapping:IEntityTypeConfiguration<Article>
	{
		public void Configure(EntityTypeBuilder<Article> builder)
		{
			builder.ToTable("Articles");
			builder.HasKey(x => x.Id);

			builder.Property(x => x.Title).HasMaxLength(255).IsRequired();
			builder.Property(x => x.ShortDescription).HasMaxLength(800).IsRequired();
			builder.Property(x => x.Description).IsRequired();
			builder.Property(x => x.Picture).HasMaxLength(500);
			builder.Property(x => x.PictureAlt).HasMaxLength(200);
			builder.Property(x => x.PictureTitle).HasMaxLength(200);
			builder.Property(x => x.Slug).HasMaxLength(255).IsRequired();
			builder.Property(x => x.Keywords).HasMaxLength(200);
			builder.Property(x => x.MetaDescription).HasMaxLength(500);
			builder.Property(x => x.CanonicalAddress).HasMaxLength(800);

			builder.HasOne(x=>x.ArticleCategory).WithMany(x=>x.Articles)
				.HasForeignKey(x=>x.CategoryId);
		}
	}
}
