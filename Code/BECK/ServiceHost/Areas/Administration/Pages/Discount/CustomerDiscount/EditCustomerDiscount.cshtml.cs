using _0_Framework.Infrastructure;
using DiscountManagement.Application.Contract.CustomerDiscount;
using DiscountManagement.Configuration.Permission;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contract.Product;

namespace ServiceHost.Areas.Administration.Pages.Discount.CustomerDiscount
{
    public class EditCustomerDiscountModel : PageModel
    {
        public string Message {  get; set; }
        public EditCustomerDiscount EditCustomerDiscount { get; set; }
        public SelectList Products { get; set; }
        private readonly ICustomerDiscountApplication _customerDiscountApplication;
        private readonly IProductApplication _productApplication;

        public EditCustomerDiscountModel(ICustomerDiscountApplication customerDiscountApplication,IProductApplication productApplication)
        {
            _customerDiscountApplication = customerDiscountApplication;
            _productApplication = productApplication;
        }

        [NeedsPermissions(DiscountPermission.EditCustomerDiscount)]
        public void OnGet(long id,string message)
        {
            EditCustomerDiscount=_customerDiscountApplication.GetDetails(id);
            Products=new SelectList(_productApplication.GetProducts(),"Id","Name");
            Message = message;
        }

        [NeedsPermissions(DiscountPermission.EditCustomerDiscount)]
        public IActionResult OnPost(EditCustomerDiscount editCustomerDiscount)
        {
            var operation = _customerDiscountApplication.Edit(editCustomerDiscount);
            if (operation.IsSuccedded)
            {
                return RedirectToPage("Index");
            }
            else
            {
                return RedirectToAction("Get", new { editCustomerDiscount.Id,operation.Message });
            }
        }
    }
}
