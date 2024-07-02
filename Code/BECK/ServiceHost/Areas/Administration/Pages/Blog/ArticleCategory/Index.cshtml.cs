using _0_Framework.Infrastructure;
using BlogManagement.Application.Contract.ArticleCategory;
using BlogManagement.Configuration.Permission;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.Blog.ArticleCategory
{
	public class IndexModel : PageModel
    {
		public List<ArticleCategoryViewModel> ArticleCategories { get; set; }
		public string ApiPath {  get; set; }
        private readonly IArticleCategoryApplication _articleCategoryApplication;
		private readonly IConfiguration _configuration;

        public IndexModel(IArticleCategoryApplication articleCategoryApplication, IConfiguration configuration)
        {
            _articleCategoryApplication = articleCategoryApplication;
            _configuration = configuration;
            ApiPath = _configuration.GetSection("WebInfo")["UrlOFApi"];
        }

        [NeedsPermissions(BlogPermission.ListArticleCategory)]
        public void OnGet(ArticleCategorySearchModel searchModel)
        {
			ArticleCategories = _articleCategoryApplication.Search(searchModel);
        }
	}
}
