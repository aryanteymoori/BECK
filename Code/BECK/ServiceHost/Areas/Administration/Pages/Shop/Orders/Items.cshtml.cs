using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contract.Order;

namespace ServiceHost.Areas.Administration.Pages.Shop.Orders
{
    public class ItemsModel : PageModel
    {
        public List<OrderItemViewModel> OrderItems { get; set; }
        private readonly IOrderApplication _orderApplication;

        public ItemsModel(IOrderApplication orderApplication)
        {
            _orderApplication = orderApplication;
        }

        public void OnGet(long id)
        {
            OrderItems = _orderApplication.SearchItems(id);
        }
    }
}
