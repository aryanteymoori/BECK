using _01_BECKQuery.Contract.Slide;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace ServiceHost.ViewComponents
{
	public class SlideViewComponent : ViewComponent
	{
		private readonly ISlideQuery _slideQuery;

		public SlideViewComponent(ISlideQuery slideQuery)
		{
			_slideQuery = slideQuery;
		}

		public IViewComponentResult Invoke()
		{
			return View(_slideQuery.GetSlides());
		}
	}
}
