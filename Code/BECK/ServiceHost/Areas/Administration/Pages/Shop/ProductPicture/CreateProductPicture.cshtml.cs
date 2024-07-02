using _0_Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contract.Product;
using ShopManagement.Application.Contract.ProductPicture;
using ShopManagement.Configuration.Permissions;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductPicture
{
    public class CreateProductPictureModel : PageModel
    {
        public string Message {  get; set; }
        public CreateProductPicture CreateProductPicture {  get; set; }
        public SelectList Products { get; set; }
        private readonly IProductPictureApplication _productPictureApplication;
        private readonly IProductApplication _productApplication;

		public CreateProductPictureModel(IProductPictureApplication productPictureApplication,IProductApplication productApplication)
		{
			_productPictureApplication = productPictureApplication;
            _productApplication = productApplication;
		}

        [NeedsPermissions(ShopPermissions.CreateProductPicture)]
		public void OnGet(string message)
        {
            Message = message;
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
        }

        [NeedsPermissions(ShopPermissions.CreateProductPicture)]
        public IActionResult OnPost(CreateProductPicture createProductPicture) 
        {
            if (ModelState.IsValid)
            {
				var operation = _productPictureApplication.Create(createProductPicture);
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
                return RedirectToPage("CreateProductPicture");
            }
        }
    }
}
