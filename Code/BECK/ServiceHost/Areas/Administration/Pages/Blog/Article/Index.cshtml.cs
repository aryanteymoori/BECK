using _0_Framework.Infrastructure;
using BlogManagement.Application.Contract.Article;
using BlogManagement.Configuration.Permission;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.Blog.Article
{
    public class IndexModel : PageModel
    {
        public List<ArticleViewModel> Articles { get; set; }
        public string ApiPath {  get; set; }
        private readonly IArticleApplication _articleApplication;
        private readonly IConfiguration _configuration;

        public IndexModel(IArticleApplication articleApplication, IConfiguration configuration)
        {
            _articleApplication = articleApplication;
            _configuration = configuration;
            ApiPath = _configuration.GetSection("WebInfo")["UrlOFApi"];
        }

        [NeedsPermissions(BlogPermission.ListArticle)]
		public void OnGet(ArticleSearchModel searchModel)
        {
            Articles = _articleApplication.Search(searchModel);
        }
	}
}
