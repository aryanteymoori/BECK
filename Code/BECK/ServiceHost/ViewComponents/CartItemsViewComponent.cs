using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class CartItemsViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
