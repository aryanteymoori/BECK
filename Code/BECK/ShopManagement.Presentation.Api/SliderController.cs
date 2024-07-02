using Microsoft.AspNetCore.Mvc;
using ShopManagement.Application.Contract.Slider;

namespace ShopManagement.Presentation.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SliderController : ControllerBase
    {
        private readonly ISlideApplication _slideApplication;

        public SliderController(ISlideApplication slideApplication)
        {
            _slideApplication = slideApplication;
        }

        [HttpPost("DeleteSlider/{id}")]
        public SliderState DeleteSlider(long id)
        {
            _slideApplication.Delete(id);
            return _slideApplication.GetSliderState(id);
        }

        [HttpPost("RestoreSlider/{id}")]
        public SliderState RestoreSlider(long id)
        {
            _slideApplication.Restore(id);
            return _slideApplication.GetSliderState(id);
        }
    }
}
