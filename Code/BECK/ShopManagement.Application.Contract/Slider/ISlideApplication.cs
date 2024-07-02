using _0_Framework.Application;

namespace ShopManagement.Application.Contract.Slider
{
	public interface ISlideApplication
	{
		OperationResult Create(CreateSlide command);
		OperationResult Edit(EditSlide command);
		OperationResult Restore(long id);
		OperationResult Delete(long id);
		EditSlide GetDetails(long id);
		List<SlideViewModel> Search();
		SliderState GetSliderState(long id);
	}
}
