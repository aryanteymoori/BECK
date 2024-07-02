using _0_Framework.Infrastructure;
using DiscountManagement.Application.Contract.ColleagueDiscount;
using DiscountManagement.Configuration.Permission;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contract.Product;

namespace ServiceHost.Areas.Administration.Pages.Discount.ColleagueDiscount
{
    public class EditColleagueDiscountModel : PageModel
    {
        public string Message {  get; set; }
        public SelectList Products { get; set; }
        public EditColleagueDiscount EditColleagueDiscount { get; set; }
        private readonly IColleagueDiscountApplication _colleagueDiscountApplication;
        private readonly IProductApplication _productApplication;

        public EditColleagueDiscountModel(IColleagueDiscountApplication colleagueDiscountApplication, IProductApplication productApplication)
        {
            _colleagueDiscountApplication = colleagueDiscountApplication;
            _productApplication = productApplication;
        }

        [NeedsPermissions(DiscountPermission.EditColleagueDiscount)]
        public void OnGet(long id,string message)
        {
            EditColleagueDiscount= _colleagueDiscountApplication.GetDetails(id);
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            Message = message;
        }

        [NeedsPermissions(DiscountPermission.EditColleagueDiscount)]
        public IActionResult OnPost(EditColleagueDiscount editColleagueDiscount)
        {
            var operation = _colleagueDiscountApplication.Edit(editColleagueDiscount);
            if (operation.IsSuccedded)
            {
                return RedirectToPage("Index");
            }
            else
            {
                return RedirectToAction("Get", new { editColleagueDiscount.Id,operation.Message });
            }
        }
    }
}
