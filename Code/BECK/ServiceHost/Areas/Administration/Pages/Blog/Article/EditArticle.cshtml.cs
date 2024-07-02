using _0_Framework.Infrastructure;
using BlogManagement.Application.Contract.Article;
using BlogManagement.Application.Contract.ArticleCategory;
using BlogManagement.Configuration.Permission;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics.Contracts;
using System.Xml.Serialization;

namespace ServiceHost.Areas.Administration.Pages.Blog.Article
{
    public class EditArticleModel : PageModel
    {
        public string Message {  get; set; }
        public EditArticle EditArticle { get; set; }
		public SelectList CategoryId {  get; set; }
        private readonly IArticleApplication _articleApplication;
		private readonly IArticleCategoryApplication _articleCategoryApplication;

		public EditArticleModel(IArticleApplication articleApplication,IArticleCategoryApplication articleCategoryApplication)
		{
			_articleApplication = articleApplication;
			_articleCategoryApplication = articleCategoryApplication;
		}

		[NeedsPermissions(BlogPermission.EditArticle)]
		public void OnGet(long id,string message)
        {
            EditArticle = _articleApplication.GetDetails(id);
			CategoryId = new SelectList(_articleCategoryApplication.GetArticleCatgories(),"Id","Name");
            Message = message;
        }

		[NeedsPermissions(BlogPermission.EditArticle)]
		public IActionResult OnPost(EditArticle editArticle)
        {
			if (ModelState.IsValid)
			{
				var operation = _articleApplication.Edit(editArticle);
				if (operation.IsSuccedded)
				{
					return RedirectToPage("Index");
				}
				else
				{
					return RedirectToAction("Get", new { editArticle.Id,operation.Message });
				}

			}
			else
			{
				return RedirectToPage("CreateArticle");
			}
		}
    }
}
