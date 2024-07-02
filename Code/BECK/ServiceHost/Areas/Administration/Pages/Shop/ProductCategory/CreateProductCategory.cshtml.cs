using _0_Framework.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contract.ProductCategory;
using ShopManagement.Configuration.Permissions;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductCategory
{
	//[Authorize(Roles = "1")]
	public class CreateProductCategoryModel : PageModel
    {
        public string Message {  get; set; }
        public CreateProductCategory CreateProductCategory { get; set; }
        private readonly IProductCategoryApplication _productCategoryApplication;

		public CreateProductCategoryModel(IProductCategoryApplication productCategoryApplication)
		{
			_productCategoryApplication = productCategoryApplication;
		}

		[NeedsPermissions(ShopPermissions.CreateProductCategory)]
		public void OnGet(CreateProductCategory createProductCategory,string message)
        {
            Message = message;
            CreateProductCategory=createProductCategory;
        }

		[NeedsPermissions(ShopPermissions.CreateProductCategory)]
		public IActionResult OnPost(CreateProductCategory createProductCategory) 
        {
            if (ModelState.IsValid)
            {
				var operation = _productCategoryApplication.Create(createProductCategory);
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
				return RedirectToPage("CreateProductCategory");
			}
        }
    }
}
