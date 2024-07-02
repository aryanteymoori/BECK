using _0_Framework.Infrastructure;
using DiscountManagement.Application.Contract.ColleagueDiscount;
using DiscountManagement.Configuration.Permission;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.Discount.ColleagueDiscount
{
    public class IndexModel : PageModel
    {
        public List<ColleagueDiscountViewModel> ColleagueDiscounts { get; set; }
        public string ApiPath {  get; set; }
        private readonly IColleagueDiscountApplication _colleagueDiscountApplication;
        private readonly IConfiguration _configuration;

        public IndexModel(IColleagueDiscountApplication colleagueDiscountApplication, IConfiguration configuration)
        {
            _colleagueDiscountApplication = colleagueDiscountApplication;
            _configuration = configuration;
            ApiPath = _configuration.GetSection("WebInfo")["UrlOFApi"];
        }

        [NeedsPermissions(DiscountPermission.ListColleagueDiscount)]
		public void OnGet(ColleagueDiscountSearchModel searchModel)
        {
            ColleagueDiscounts = _colleagueDiscountApplication.Search(searchModel);
        }
	}
}
