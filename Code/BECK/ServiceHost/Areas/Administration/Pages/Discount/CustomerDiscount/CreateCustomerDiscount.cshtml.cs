using _0_Framework.Infrastructure;
using DiscountManagement.Application.Contract.CustomerDiscount;
using DiscountManagement.Configuration.Permission;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contract.Product;

namespace ServiceHost.Areas.Administration.Pages.Discount.CustomerDiscount
{
    public class CreateCustomerDiscountModel : PageModel
    {
        public string Message {  get; set; }
        public CreateCustomerDiscount CreateCustomerDiscount { get; set; }
        public SelectList Products { get; set; }
        private readonly IProductApplication _productApplication;
        private readonly ICustomerDiscountApplication _customerDiscountApplication;

		public CreateCustomerDiscountModel(ICustomerDiscountApplication customerDiscountApplication,IProductApplication productApplication)
		{
			_customerDiscountApplication = customerDiscountApplication;
            _productApplication = productApplication;
		}

        [NeedsPermissions(DiscountPermission.CreateCustomerDiscount)]
		public void OnGet(string message)
        {
            Message = message;
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
        }

        [NeedsPermissions(DiscountPermission.CreateCustomerDiscount)]
        public IActionResult OnPost(CreateCustomerDiscount createCustomerDiscount)
        {
            var operation = _customerDiscountApplication.Create(createCustomerDiscount);
            if (operation.IsSuccedded)
            {
                return RedirectToPage("Index");
            }
            else
            {
                return RedirectToAction("Get", new { operation.Message });
            }
        }
    }
}
