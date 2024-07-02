using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contract.Order;

namespace ServiceHost.Areas.Administration.Pages.Shop.Orders
{
    public class IndexModel : PageModel
    {
        public List<OrderViewModel> Orders { get; set; }
        public string ApiPath { get; set; }
        private readonly IOrderApplication _orderApplication;
        private readonly IConfiguration _configuration;

        public IndexModel(IOrderApplication orderApplication, IConfiguration configuration)
        {
            _orderApplication = orderApplication;
            _configuration = configuration;
            ApiPath = _configuration.GetSection("WebInfo")["UrlOFApi"];
        }

        public void OnGet(OrderSearchModel searchModel)
        {
            Orders = _orderApplication.Search(searchModel);
        }
    }
}
