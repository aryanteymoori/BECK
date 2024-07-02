using _0_Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contract.ProductPicture;
using ShopManagement.Configuration.Permissions;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductPicture
{
    public class IndexModel : PageModel
    {
        public List<ProductPictureViewModel> ProductPictures { get; set; }
        public string ApiPath { get; set; }
        private readonly IProductPictureApplication _productPictureApplication;
        private readonly IConfiguration _configuration;

        public IndexModel(IProductPictureApplication productPictureApplication, IConfiguration configuration)
        {
            _productPictureApplication = productPictureApplication;
            _configuration = configuration;
            ApiPath = _configuration.GetSection("WebInfo")["UrlOFApi"];
        }

        [NeedsPermissions(ShopPermissions.ListProductPicture)]
		public void OnGet(ProductPictureSearchModel searchModel)
        {
            ProductPictures = _productPictureApplication.Search(searchModel);
        }
    }
}
