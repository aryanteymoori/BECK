using _0_Framework.Application;
using _0_Framework.Infrastructure;
using ShopManagement.Application.Contract.Slider;
using ShopManagement.Domain.SliderAgg;
using System.Linq.Expressions;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class SlideRepository : RepositoryBase<Slide, long>, ISlideRepository
	{
		private readonly ShopContext _shopContext;

		public SlideRepository(ShopContext shopContext):base(shopContext)
		{
			_shopContext = shopContext;
		}

		public EditSlide GetDetails(long id)
		{
			return _shopContext.Slides.Select(x => new EditSlide
			{
				Id = x.Id,
				//Picture = x.Picture,
				PictureAlt = x.PictureAlt,
				PictureTitle = x.PictureTitle,
				Heading = x.Heading,
				Title = x.Title,
				Text = x.Text,
				BtnText = x.BtnText,
				Link=x.Link
			}).FirstOrDefault(x => x.Id == id);
		}

        public SliderState GetSliderState(long id)
        {
			return _shopContext.Slides.Select(x => new SliderState
			{
				Id = x.Id,
				IsDeleted = x.IsDeleted,
			}).FirstOrDefault(x => x.Id == id);
        }

        public List<SlideViewModel> Search()
		{
			return _shopContext.Slides.Select(x=>new SlideViewModel
			{
				Id=x.Id,
				Heading=x.Heading,
				Picture=x.Picture,
				Title=x.Title,
				CreationDate=x.CreateionDate.ToFarsi(),
				IsDeleted=x.IsDeleted
			}).OrderByDescending(x=>x.Id).ToList();
		}
	}
}
