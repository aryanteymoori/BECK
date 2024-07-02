using _0_Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contract.Product;
using ShopManagement.Application.Contract.ProductCategory;
using ShopManagement.Configuration.Permissions;

namespace ServiceHost.Areas.Administration.Pages.Shop.Product
{
    public class EditProductModel : PageModel
    {
        public string Message {  get; set; }
        public SelectList ProductCategories { get; set; }
        public EditProduct EditProduct { get; set; }
        private readonly IProductApplication _productApplication;
        private readonly IProductCategoryApplication _categoryApplication;

		public EditProductModel(IProductApplication productApplication,IProductCategoryApplication productCategoryApplication)
		{
			_productApplication = productApplication;
            _categoryApplication = productCategoryApplication;
		}
		[NeedsPermissions(ShopPermissions.EditProduct)]
		public void OnGet(long id,string message)
        {
            EditProduct=_productApplication.GetDetails(id);
			ProductCategories = new SelectList(_categoryApplication.GetProductCategories(), "Id", "Name");
            Message = message;
        }
        [NeedsPermissions(ShopPermissions.EditProduct)]
        public IActionResult OnPost(EditProduct editProduct)
        {
            if (ModelState.IsValid)
            {
				var operation = _productApplication.Edit(editProduct);
				if (operation.IsSuccedded)
				{
					return RedirectToPage("Index");
				}
				else
				{
					return RedirectToAction("Get", new { editProduct.Id, operation.Message });
				}
			}
            else
            {
                return RedirectToPage("EditProduct", new {editProduct.Id});
            }
        }
    }
}
