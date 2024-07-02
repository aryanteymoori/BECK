using Microsoft.AspNetCore.Mvc;
using ShopManagement.Application.Contract.ProductPicture;
namespace ShopManagement.Presentation.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductPictureController : ControllerBase
    {
        private readonly IProductPictureApplication _productPictureApplication;

        public ProductPictureController(IProductPictureApplication productPictureApplication)
        {
            _productPictureApplication = productPictureApplication;
        }

        [HttpPost("DeleteProductPicture/{id}")]
        public ProductPictureState DeleteProductPicture(long id)
        {
            _productPictureApplication.Delete(id);
            return _productPictureApplication.GetProductPictureState(id);
        }


        [HttpPost("RestoreProductPicture/{id}")]
        public ProductPictureState RestoreProductPicture(long id)
        {
            _productPictureApplication.Restore(id);
            return _productPictureApplication.GetProductPictureState(id);
        }
    }
}
