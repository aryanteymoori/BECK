using _0_Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contract.Product;
using ShopManagement.Configuration.Permissions;

namespace ServiceHost.Areas.Administration.Pages.Shop.Product
{
    public class IndexModel : PageModel
    {
        public List<ProductViewModel> Producs { get; set; }
        private readonly IProductApplication _productApplication;

		public IndexModel(IProductApplication productApplication)
		{
			_productApplication = productApplication;
		}
        [NeedsPermissions(ShopPermissions.ListProduct)]
		public void OnGet(ProductSearchModel searchModel)
        {
            Producs=_productApplication.Search(searchModel);
        }
	}
}
