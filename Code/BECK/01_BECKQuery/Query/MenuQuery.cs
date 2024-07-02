using _01_BECKQuery.Contract.ArticleCategory;
using _01_BECKQuery.Contract.Menu;
using _01_BECKQuery.Contract.ProductCategory;
using BlogManagement.Infrastructure.EFCore;
using ShopManagement.Infrastructure.EFCore;

namespace _01_BECKQuery.Query
{
    public class MenuQuery : IMenuQuery
    {
        private readonly BlogContext _blogContext;
        private readonly ShopContext _shopContext;

        public MenuQuery(BlogContext blogContext, ShopContext shopContext)
        {
            _blogContext = blogContext;
            _shopContext = shopContext;
        }

        public MenuQueryModel GetMenuDetails()
        {
            return new MenuQueryModel
            {
                ArticleCategories = _blogContext.articleCategories.Where(x=>!x.IsDeleted).Select(x=>new ArticleCategoryQueryModel{Id = x.Id,Slug=x.Slug,Name=x.Name}).ToList(),
                ProductCategories = _shopContext.ProductCategories.Select(x=>new ProductCategoryQueryModel{Id = x.Id,Slug=x.Slug,Name=x.Name }).ToList(),
            };
        }
    }
}
