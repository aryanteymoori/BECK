using _0_Framework.Application;
using _01_BECKQuery.Contract.Article;
using _01_BECKQuery.Contract.ArticleCategory;
using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;

namespace _01_BECKQuery.Query
{
    public class ArticleCategoryQuery : IArticleCategoryQuery
    {
        private readonly BlogContext _blogContext;
        public ArticleCategoryQuery(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }

        public ArticleCategoryQueryModel GetArticleCategoryBy(string slug)
        {
            return _blogContext.articleCategories.Where(x => !x.IsDeleted)
                .Include(x=>x.Articles)
                .ThenInclude(x=>x.ArticleCategory)
                .Select(x => new ArticleCategoryQueryModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                CanonicalAddress = x.CanonicalAddress,
                Picture = x.Picture,
                MetaDescription = x.MetaDescription,
                Slug = x.Slug,
                Keywords = x.Keywords,
                Articles=MapArticles(x.Articles)
            }).FirstOrDefault(x => x.Slug == slug);
        }

        private static List<ArticleQueryModel> MapArticles(List<Article> articles)
        {
            return articles.Where(x=>!x.IsDeleted).Select(x=>new ArticleQueryModel
            {
                Id=x.Id,
                Title = x.Title,
                ShortDescription = x.ShortDescription,
                Description = x.Description,
                CanonicalAddress=x.CanonicalAddress,
                CategoryId = x.CategoryId,
                CategoryName=x.ArticleCategory.Name,
                CreationDate=x.CreateionDate.ToFarsi(),
                Keywords=x.Keywords,
                MetaDescription=x.MetaDescription,
                Picture=x.Picture,
                PictureAlt=x.PictureAlt,
                PictureTitle=x.PictureTitle,
                Slug=x.Slug,
                CategorySlug=x.ArticleCategory.Slug,
            }).ToList();
        }
    }
}
