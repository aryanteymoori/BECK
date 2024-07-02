using _0_Framework.Domain;
using ShopManagement.Application.Contract.Slider;

namespace ShopManagement.Domain.SliderAgg
{
	public interface ISlideRepository:IRepository<Slide,long>
	{
		EditSlide GetDetails(long id);
		List<SlideViewModel> Search();
        SliderState GetSliderState(long id);
    }
}
