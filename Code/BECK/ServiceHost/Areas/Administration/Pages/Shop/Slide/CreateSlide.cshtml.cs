using _0_Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contract.Slider;
using ShopManagement.Configuration.Permissions;

namespace ServiceHost.Areas.Administration.Pages.Shop.Slide
{
    public class CreateSlideModel : PageModel
    {
        public string Message {  get; set; }
        public CreateSlide CreateSlide { get; set; }
        private readonly ISlideApplication _slideApplication;

		public CreateSlideModel(ISlideApplication slideApplication)
		{
			_slideApplication = slideApplication;
		}

		[NeedsPermissions(ShopPermissions.CreateSlider)]
		public void OnGet(string message)
        {
            Message = message;
        }

		[NeedsPermissions(ShopPermissions.CreateSlider)]
		public IActionResult OnPost(CreateSlide createSlide) 
        {
            if (ModelState.IsValid)
            {
				var operation = _slideApplication.Create(createSlide);
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
				return RedirectToPage("CreateSlide");
			}
        }
    }
}
