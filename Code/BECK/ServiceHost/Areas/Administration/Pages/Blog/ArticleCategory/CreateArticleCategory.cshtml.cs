using _0_Framework.Infrastructure;
using BlogManagement.Application.Contract.ArticleCategory;
using BlogManagement.Configuration.Permission;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.Blog.ArticleCategory
{
	public class CreateArticleCategoryModel : PageModel
	{
		public string Message {  get; set; }
		public CreateArticleCategory CreateArticleCategory { get; set; }
		private readonly IArticleCategoryApplication _articleCategoryApplication;

		public CreateArticleCategoryModel(IArticleCategoryApplication articleCategoryApplication)
		{
			_articleCategoryApplication = articleCategoryApplication;
		}


        [NeedsPermissions(BlogPermission.CreateArticleCategory)]
        public void OnGet(string message)
		{
			Message=message;
		}

        [NeedsPermissions(BlogPermission.CreateArticleCategory)]
        public IActionResult OnPost(CreateArticleCategory createArticleCategory) 
		{
			if (ModelState.IsValid)
			{
				var operation = _articleCategoryApplication.Create(createArticleCategory);
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
				return RedirectToAction("Get");
			}
		}
	}
}
