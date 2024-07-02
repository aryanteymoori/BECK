using _0_Framework.Application;
using _0_Framework.Infrastructure;
using BlogManagement.Application.Contract.Article;
using BlogManagement.Domain.ArticleAgg;
using Microsoft.EntityFrameworkCore;

namespace BlogManagement.Infrastructure.EFCore.Repository
{
	public class ArticleRepository : RepositoryBase<Article, long>, IArticleRepository
	{
		private readonly BlogContext _blogContext;

		public ArticleRepository(BlogContext blogContext):base(blogContext)
		{
			_blogContext = blogContext;
		}

        public ArticleState GetArticleStateBy(long id)
        {
			return _blogContext.Articles.Select(x => new ArticleState
			{
				Id = x.Id,
				IsDeleted = x.IsDeleted
			}).FirstOrDefault(x => x.Id == id);
        }

        public EditArticle GetDetails(long id)
		{
			return _blogContext.Articles.Select(x => new EditArticle
			{
				Id = x.Id,
				Title = x.Title,
				ShortDescription = x.ShortDescription,
				Description = x.Description,
				//Picture = x.Picture,
				PictureAlt = x.PictureAlt??"",
				PictureTitle = x.PictureTitle??"",
				Slug = x.Slug,
				CategoryId = x.CategoryId,
				CanonicalAddress = x.CanonicalAddress ?? "",
				Keywords = x.Keywords ?? "",
				MetaDescription = x.MetaDescription ?? ""
			}).FirstOrDefault(x => x.Id == id);
		}

		public Article GetWithCategoryBy(long id)
		{
			return _blogContext.Articles.Include(x=>x.ArticleCategory).FirstOrDefault(x => x.Id == id);
		}

		public List<ArticleViewModel> Search(ArticleSearchModel searchModel)
		{
			var query = _blogContext.Articles.Include(x => x.ArticleCategory).Select(x => new ArticleViewModel
			{
				Id = x.Id,
				Title = x.Title,
				IsDeleted = x.IsDeleted,
				Picture = x.Picture,
				ShortDescription = x.ShortDescription,
				CategoryName = x.ArticleCategory.Name,
				CategoryId = x.CategoryId,
				CreationDate = x.CreateionDate.ToFarsi()
			});

			if (!string.IsNullOrWhiteSpace(searchModel.Title))
				query = query.Where(x => x.Title.Contains(searchModel.Title));
			if (searchModel.CategoryId > 0)
				query = query.Where(x => x.CategoryId == searchModel.CategoryId);

			return query.OrderByDescending(x => x.Id).ToList();
		}
	}
}
