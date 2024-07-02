using _0_Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Identity.Client;
using ShopManagement.Application.Contract.Product;
using ShopManagement.Application.Contract.ProductPicture;
using ShopManagement.Configuration.Permissions;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductPicture
{
    public class EditProductPictureModel : PageModel
    {
        public string Message {  get; set; }
        public EditProductPicture EditProductPicture { get; set; }
        public SelectList Products { get; set; }
        private readonly IProductPictureApplication _productPictureApplication;
        private readonly IProductApplication _productApplication;

		public EditProductPictureModel(IProductPictureApplication productPictureApplication,IProductApplication productApplication)
		{
			_productPictureApplication = productPictureApplication;
            _productApplication = productApplication;
		}

        [NeedsPermissions(ShopPermissions.EditProductPicture)]
        public void OnGet(long id,string message)
        {
            EditProductPicture=_productPictureApplication.GetDetails(id);
            Message = message;
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
        }


        [NeedsPermissions(ShopPermissions.EditProductPicture)]
        public IActionResult OnPost(EditProductPicture editProductPicture) 
        {
            if (ModelState.IsValid)
            {
				var operation = _productPictureApplication.Edit(editProductPicture);
				if (operation.IsSuccedded)
				{
					return RedirectToPage("Index");
				}
				else
				{
					return RedirectToAction("Get", new { editProductPicture.Id, operation.Message });
				}
            }
            else
            {
                return RedirectToPage("EditProductPicture", new {editProductPicture.Id});
            }
        }
    }
}
