using _0_Framework.Infrastructure;
using DiscountManagement.Application.Contract.CustomerDiscount;
using DiscountManagement.Configuration.Permission;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.Discount.CustomerDiscount
{
    public class IndexModel : PageModel
    {
        public List<CustomerDiscountViewModel> CustomerDiscounts { get; set; }
        private readonly ICustomerDiscountApplication _customerDiscountApplication;

		public IndexModel(ICustomerDiscountApplication customerDiscountApplication)
		{
			_customerDiscountApplication = customerDiscountApplication;
		}

        [NeedsPermissions(DiscountPermission.ListCustomerDiscount)]
		public void OnGet(CustomerDiscountSearchModel searchModel)
        {
			CustomerDiscounts = _customerDiscountApplication.Search(searchModel);
        }
    }
}
