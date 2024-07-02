using _0_Framework.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contract.ProductCategory;
using ShopManagement.Configuration.Permissions;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductCategory
{
    //[Authorize(Roles = "1")]
    public class EditProductCategoryModelModel : PageModel
    {
        public string Message {  get; set; }
        public EditProductCategory EditProductCategory { get; set; }
        private readonly IProductCategoryApplication _productCategoryApplication;

		public EditProductCategoryModelModel(IProductCategoryApplication productCategoryApplication)
		{
			_productCategoryApplication = productCategoryApplication;
		}

        [NeedsPermissions(ShopPermissions.EditProductCategory)]
		public void OnGet(long id,string message)
        {
            EditProductCategory=_productCategoryApplication.GetDetails(id);
            Message = message;
        }


        [NeedsPermissions(ShopPermissions.EditProductCategory)]
        public IActionResult OnPost(EditProductCategory editProductCategory) 
        {
            if (ModelState.IsValid)
            {
				var operation = _productCategoryApplication.Edit(editProductCategory);
				if (operation.IsSuccedded)
				{
					return RedirectToPage("Index");
				}
				else
				{
					return RedirectToAction("Get", new { editProductCategory.Id, operation.Message });
				}
			}
            else
            {
                return RedirectToPage("EditProductCategoryModel", new { editProductCategory.Id }); 
            }
        }
    }
}
