using _01_BECKQuery.Contract.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ArticleCategoriesModel : PageModel
    {
        public ArticleCategoryQueryModel ArticleCategory {  get; set; }
        private readonly IArticleCategoryQuery _articleCategoryQuery;

        public ArticleCategoriesModel(IArticleCategoryQuery articleCategoryQuery)
        {
            _articleCategoryQuery = articleCategoryQuery;
        }

        public void OnGet(string slug)
        {
            ArticleCategory = _articleCategoryQuery.GetArticleCategoryBy(slug);
        }
    }
}
