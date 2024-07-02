using _0_Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contract.Product;
using ShopManagement.Application.Contract.ProductCategory;
using ShopManagement.Configuration.Permissions;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ServiceHost.Areas.Administration.Pages.Shop.Product
{
    public class CreateProductModel : PageModel
    {
        public string Message {  get; set; }
        public SelectList ProductCategories { get; set; }
        public CreateProduct CreateProduct { get; set; }
        private readonly IProductApplication _productApplication;
        private readonly IProductCategoryApplication _productCategoryApplication;

		public CreateProductModel(IProductApplication productApplication, IProductCategoryApplication productCategoryApplication)
		{
			_productApplication = productApplication;
			_productCategoryApplication = productCategoryApplication;
		}

		[NeedsPermissions(ShopPermissions.CreateProduct)]
		public void OnGet(string message)
        {
            ProductCategories = new SelectList(_productCategoryApplication.GetProductCategories(), "Id", "Name");
            Message = message;
        }

        [NeedsPermissions(ShopPermissions.CreateProduct)]
        public IActionResult OnPost(CreateProduct createProduct) 
        {
            if (ModelState.IsValid)
            {
				var operation = _productApplication.Create(createProduct);
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
                return RedirectToPage("CreateProduct");
            }
        }
    }
}
