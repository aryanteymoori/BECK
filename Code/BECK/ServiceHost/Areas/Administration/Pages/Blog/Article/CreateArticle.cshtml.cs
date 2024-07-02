using _0_Framework.Application;
using _0_Framework.Infrastructure;
using BlogManagement.Application.Contract.Article;
using BlogManagement.Application.Contract.ArticleCategory;
using BlogManagement.Configuration.Permission;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Blog.Article
{
    public class CreateArticleModel : PageModel
    {
        public string Message {  get; set; }
        public SelectList CategoryId { get; set; }
        public CreateArticle CreateArticle { get; set; }
        private readonly IArticleApplication _articleApplication;
        private readonly IArticleCategoryApplication _articleCategoryApplication;

		public CreateArticleModel(IArticleApplication articleApplication,IArticleCategoryApplication articleCategoryApplication)
		{
			_articleApplication = articleApplication;
            _articleCategoryApplication = articleCategoryApplication;
		}

		[NeedsPermissions(BlogPermission.CreateArticle)]
		public void OnGet(string message)
        {
            Message = message;
            CategoryId = new SelectList(_articleCategoryApplication.GetArticleCatgories(),"Id","Name");
        }

		[NeedsPermissions(BlogPermission.CreateArticle)]
		public IActionResult OnPost(CreateArticle createArticle) 
        {
            if (ModelState.IsValid)
            {
                var operation = _articleApplication.Create(createArticle);
                if (operation.IsSuccedded)
                {
                    return RedirectToPage("Index");
                }
                else
                {
                    return RedirectToAction("Get", new { operation.Message });
                }

            }
            else
            {
                return RedirectToPage("CreateArticle");
            }
        }
    }
}
