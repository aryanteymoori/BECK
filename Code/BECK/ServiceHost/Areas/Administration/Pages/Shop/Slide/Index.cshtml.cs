using _0_Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contract.Slider;
using ShopManagement.Configuration.Permissions;

namespace ServiceHost.Areas.Administration.Pages.Shop.Slide
{
    public class IndexModel : PageModel
    {
        public List<SlideViewModel> SlideViewModel { get; set; }
        public string ApiPath { get; set; }
        private readonly ISlideApplication _slideApplication;
        private readonly IConfiguration _configuration;

        public IndexModel(ISlideApplication slideApplication, IConfiguration configuration)
        {
            _slideApplication = slideApplication;
            _configuration = configuration;
            ApiPath = _configuration.GetSection("WebInfo")["UrlOFApi"];
        }

        [NeedsPermissions(ShopPermissions.ListSlider)]
		public void OnGet()
        {
            SlideViewModel = _slideApplication.Search();
        }
	}
}
