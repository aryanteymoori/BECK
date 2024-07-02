using _0_Framework.Application;
using _0_Framework.Infrastructure;
using BlogManagement.Application.Contract.ArticleCategory;
using BlogManagement.Domain.ArticleCategoryAgg;

namespace BlogManagement.Infrastructure.EFCore.Repository
{
    public class ArticleCategoryRepository : RepositoryBase<ArticleCategory, long>,IArticleCategoryRepository
    {
        private readonly BlogContext _blogContext;
        public ArticleCategoryRepository(BlogContext blogContext) : base(blogContext)
        {
            _blogContext = blogContext;
        }

        public ArticleCategoryState GetArticleCategoryStateBy(long id)
        {
            return _blogContext.articleCategories.Select(x => new ArticleCategoryState
            {
                Id = x.Id,
                IsDeleted = x.IsDeleted,
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<ArticleCategoryViewModel> GetArticleCatgories()
		{
			return _blogContext.articleCategories.Select(x => new ArticleCategoryViewModel
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();
		}

		public EditArticleCategory GetDetails(long id)
        {
            return _blogContext.articleCategories.Select(x => new EditArticleCategory
            {
                Id = x.Id,
                Name = x.Name,
                Slug = x.Slug,
                ShowOrder = x.ShowOrder,
                Description = x.Description??"",
                CanonicalAddress = x.CanonicalAddress??"",
                Keywords = x.Keywords?? "",
                MetaDescription = x.MetaDescription ?? "",
            }).FirstOrDefault(x => x.Id == id);
        }

		public string GetSlugBy(long id)
		{
            return _blogContext.articleCategories.FirstOrDefault(x => x.Id == id).Slug;
		}

		public List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel)
        {
            var query = _blogContext.articleCategories.Select(x => new ArticleCategoryViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description??"",
                ShowOrder = x.ShowOrder,
                Picture = x.Picture,
                CreationDate = x.CreateionDate.ToFarsi(),
                IsDeleted = x.IsDeleted,
                ArticlesCount=x.Articles.Count
            });
            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name.Contains(searchModel.Name));
            return query.OrderByDescending(x=>x.Id).ToList();
        }
    }
}
