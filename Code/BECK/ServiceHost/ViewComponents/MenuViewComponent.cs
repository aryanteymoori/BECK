using _01_BECKQuery.Contract.Menu;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class MenuViewComponent:ViewComponent
    {
        private readonly IMenuQuery _menuQuery;

        public MenuViewComponent(IMenuQuery menuQuery)
        {
            _menuQuery = menuQuery;
        }

        public IViewComponentResult Invoke() 
        {
            return View(_menuQuery.GetMenuDetails());
        }
    }
}
