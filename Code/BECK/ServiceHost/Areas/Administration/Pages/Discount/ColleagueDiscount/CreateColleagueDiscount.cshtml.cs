using _0_Framework.Infrastructure;
using DiscountManagement.Application.Contract.ColleagueDiscount;
using DiscountManagement.Configuration.Permission;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contract.Product;

namespace ServiceHost.Areas.Administration.Pages.Discount.ColleagueDiscount
{
    public class CreateColleagueDiscountModel : PageModel
    {
        public string Message { get; set; }
        public CreateColleagueDiscount CreateColleagueDiscount { get; set; }
        public SelectList Products { get; set; }
        private readonly IColleagueDiscountApplication _colleagueDiscountApplication;
        private readonly IProductApplication _productApplication;

        public CreateColleagueDiscountModel(IColleagueDiscountApplication colleagueDiscountApplication, IProductApplication productApplication)
        {
            _colleagueDiscountApplication = colleagueDiscountApplication;
            _productApplication = productApplication;
        }

        [NeedsPermissions(DiscountPermission.CreateColleagueDscount)]
        public void OnGet(string message)
        {
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            Message = message;
        }

        [NeedsPermissions(DiscountPermission.CreateColleagueDscount)]
        public IActionResult OnPost(CreateColleagueDiscount createColleagueDiscount) 
        {
            var operation = _colleagueDiscountApplication.Create(createColleagueDiscount);
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
