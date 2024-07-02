using _01_BECKQuery.Contract.ArticleCategory;
using _01_BECKQuery.Contract.ProductCategory;

namespace _01_BECKQuery.Contract.Menu
{
    public class MenuQueryModel
    {
        public List<ArticleCategoryQueryModel> ArticleCategories { get; set; }
        public List<ProductCategoryQueryModel> ProductCategories { get; set; }
    }
}
