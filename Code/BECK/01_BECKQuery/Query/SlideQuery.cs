using _01_BECKQuery.Contract.Slide;
using ShopManagement.Infrastructure.EFCore;

namespace _01_BECKQuery.Query
{
	public class SlideQuery : ISlideQuery
	{
		private readonly ShopContext _shopContext;

		public SlideQuery(ShopContext shopContext)
		{
			_shopContext = shopContext;
		}

		public List<SlideQueryModel> GetSlides()
		{
			return _shopContext.Slides.Where(x=>x.IsDeleted==false)
				.Select(x=>new SlideQueryModel
				{
					Picture=x.Picture,
					PictureAlt=x.PictureAlt,
					PictureTitle=x.PictureTitle,
					Heading=x.Heading,
					Title=x.Title,
					Text=x.Text,
					BtnText=x.BtnText,
					Link=x.Link
				}).ToList();
		}
	}
}
