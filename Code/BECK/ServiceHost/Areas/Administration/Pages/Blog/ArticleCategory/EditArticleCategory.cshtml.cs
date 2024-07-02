using _0_Framework.Infrastructure;
using BlogManagement.Application.Contract.ArticleCategory;
using BlogManagement.Configuration.Permission;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.Blog.ArticleCategory
{
	public class EditArticleCategoryModel : PageModel
    {
		public string Message {  get; set; }
        public EditArticleCategory EditArticleCategory { get; set; }
        private readonly IArticleCategoryApplication _articleCategoryApplication;

		public EditArticleCategoryModel(IArticleCategoryApplication articleCategoryApplication)
		{
			_articleCategoryApplication = articleCategoryApplication;
		}

        [NeedsPermissions(BlogPermission.EditArticleCategory)]
        public void OnGet(long id,string message)
        {
            EditArticleCategory=_articleCategoryApplication.GetDetails(id);
			Message = message;
        }

        [NeedsPermissions(BlogPermission.EditArticleCategory)]
        public IActionResult OnPost(EditArticleCategory editArticleCategory) 
        {
            if (ModelState.IsValid)
            {
				var operation = _articleCategoryApplication.Edit(editArticleCategory);
				if (operation.IsSuccedded)
				{
					return RedirectToPage("Index");
				}
				else
				{
					return RedirectToAction("Get", new { editArticleCategory.Id, operation.Message });
				}
			}
			else
			{
				return RedirectToAction("get", new { editArticleCategory.Id });
			}
        }
    }
}
