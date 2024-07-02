using _0_Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contract.Slider;
using ShopManagement.Configuration.Permissions;

namespace ServiceHost.Areas.Administration.Pages.Shop.Slide
{
    public class EditSlideModel : PageModel
    {
		public string Message {  get; set; }
        public EditSlide EditSlide { get; set; }
        private readonly ISlideApplication _slideApplication;

		public EditSlideModel(ISlideApplication slideApplication)
		{
			_slideApplication = slideApplication;
		}

		[NeedsPermissions(ShopPermissions.EditSlider)]
		public void OnGet(long id,string message)
        {
            EditSlide=_slideApplication.GetDetails(id);
			Message = message;
        }

		[NeedsPermissions(ShopPermissions.EditSlider)]
		public IActionResult OnPost(EditSlide editSlide) 
        {
            if (ModelState.IsValid)
            {
				var operation = _slideApplication.Edit(editSlide);
				if (operation.IsSuccedded)
				{
					return RedirectToPage("Index");
				}
				else
				{
					return RedirectToAction("Get", new { editSlide.Id, operation.Message });
				}
			}
			else
			{
				return RedirectToPage("EditSlide", new {editSlide.Id});
			}
		}
    }
}
