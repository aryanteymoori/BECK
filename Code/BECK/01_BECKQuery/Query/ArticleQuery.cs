using _0_Framework.Application;
using _01_BECKQuery.Contract.Article;
using _01_BECKQuery.Contract.Comment;
using BlogManagement.Infrastructure.EFCore;
using CommentManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductAgg;

namespace _01_BECKQuery.Query
{
    public class ArticleQuery : IArticleQuery
    {
        private readonly BlogContext _blogContext;
        private readonly CommentContext _commentContext;
        public ArticleQuery(BlogContext blogContext, CommentContext commentContext)
        {
            _blogContext = blogContext;
            _commentContext = commentContext;
        }

        public ArticleQueryModel GetArticleBy(string slug)
        {
            var article = _blogContext.Articles
                .Include(x => x.ArticleCategory)
                .Select(x => new ArticleQueryModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    ShortDescription = x.ShortDescription,
                    Description = x.Description,
                    CategoryId = x.CategoryId,
                    Keywords = x.Keywords,
                    MetaDescription = x.MetaDescription,
                    CanonicalAddress = x.CanonicalAddress,
                    Slug = x.Slug,
                    CategoryName = x.ArticleCategory.Name,
                    CategorySlug = x.ArticleCategory.Slug,
                    CreationDate = x.CreateionDate.ToFarsi(),
                }).FirstOrDefault(x => x.Slug == slug);

            var comments = _commentContext.Comments
                .Where(x => x.Type == CommentsType.Article)
                .Where(x => !x.IsCanceled)
                .Where(x => x.IsConfirmed)
                .Where(x => x.OwnerRecordId == article.Id)
                .Select(x => new CommentQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Message = x.Message,
                    ParentId = x.ParentId,
                    //ParentName = x.Parent.Name,
                    CreationDate = x.CreateionDate.ToFarsi()
                }).OrderByDescending(x => x.Id).ToList();


            foreach(var comment in comments)
            {
                if (comment.ParentId > 0)
                    comment.ParentName = comments.FirstOrDefault(x => x.Id == comment.ParentId)?.Name;
            }

            article.Comments= comments;

            return article;
        }

        public List<ArticleQueryModel> GetLatestArticles()
        {
            return _blogContext.Articles
                .Where(x => !x.IsDeleted)
                .Include(x => x.ArticleCategory)
                .Where(x => !x.ArticleCategory.IsDeleted)
                .Select(x => new ArticleQueryModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    ShortDescription = x.ShortDescription,
                    Description = x.Description,
                    CreationDate = x.CreateionDate.ToFarsi(),
                    CanonicalAddress = x.CanonicalAddress,
                    CategoryId = x.CategoryId,
                    CategoryName = x.ArticleCategory.Name,
                    Keywords = x.Keywords,
                    MetaDescription = x.MetaDescription,
                    Slug = x.Slug,
                }).OrderByDescending(x => x.Id).Take(3).ToList();
        }

        public List<ArticleQueryModel> GetArticlesBy(long categoryId)
        {

            return _blogContext.Articles.Where(x => !x.IsDeleted)
                .Where(x => x.CategoryId == categoryId)
                .Include(x => x.ArticleCategory)
                .Where(x => !x.ArticleCategory.IsDeleted)
                .Select(x => new ArticleQueryModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    ShortDescription = x.ShortDescription,
                    Description = x.Description,
                    CreationDate = x.CreateionDate.ToFarsi(),
                    CanonicalAddress = x.CanonicalAddress,
                    CategoryId = x.CategoryId,
                    CategoryName = x.ArticleCategory.Name,
                    Keywords = x.Keywords,
                    MetaDescription = x.MetaDescription,
                    Slug = x.Slug,
                }).OrderByDescending(x => x.Id).ToList();
        }
    }
}
