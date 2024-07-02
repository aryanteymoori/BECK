using _0_Framework.Application;
using _01_BECKQuery.Contract.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using ShopManagement.Application.Contract.Order;

namespace ServiceHost.Pages
{
    [Authorize]
    public class CartModel : PageModel
    {
        public string Message { get; set; }
        public string CookieName = "cart-items";
        public List<CartItem> CartItems;
        private readonly IProductQuery _productQuery;

        public CartModel(IProductQuery productQuery)
        {
            CartItems = new List<CartItem>();
            _productQuery = productQuery;
        }

        public void OnGet(string message)
        {
            var serilize = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];
            var cart = serilize.Deserialize<List<CartItem>>(value);
            CartItems = _productQuery.CheckInventoryStatus(cart);
            Message = message;
        }

        public IActionResult OnGetGoToCheckOut()
        {
            var serialize = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];
            var cart = serialize.Deserialize<List<CartItem>>(value);
            if (cart == null || cart.Count == 0)
                return RedirectToPage("Cart", new { message = "سبد خرید شما خالی است!!!" });
            CartItems = _productQuery.CheckInventoryStatus(cart);
            if (cart == null)
                return RedirectToPage("Cart");

            return RedirectToPage(CartItems.Any(x => !x.IsInStock) ? "Cart" : "CheckOut");
        }
    }
}
