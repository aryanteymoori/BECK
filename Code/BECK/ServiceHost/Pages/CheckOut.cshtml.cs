using _0_Framework.Application;
using _0_Framework.Application.ZarinPal;
using _01_BECKQuery.Contract;
using _01_BECKQuery.Contract.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using ShopManagement.Application.Contract.Order;

namespace ServiceHost.Pages
{
    public class CheckOutModel : PageModel
    {
        public const string CookieName = "cart-items";
        public Cart Cart { get; set; }
        private readonly ICartCalculatorService _cartCalculatorService;
        private readonly ICartService _cartService;
        private readonly IProductQuery _productQuery;
        private readonly IOrderApplication _orderApplication;
        private readonly IZarinPalFactory _zarinPalFactory;
        private readonly IAuthHelper _authHelper;
        public CheckOutModel(ICartCalculatorService cartCalculatorService, ICartService cartService, IProductQuery productQuery, IOrderApplication orderApplication, IZarinPalFactory zarinPalFactory, IAuthHelper authHelper)
        {
            _cartCalculatorService = cartCalculatorService;
            _cartService = cartService;
            _productQuery = productQuery;
            _orderApplication = orderApplication;
            _zarinPalFactory = zarinPalFactory;
        }

        public void OnGet()
        {
            var serialize = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];
            var cartItems = serialize.Deserialize<List<CartItem>>(value);
            Cart = _cartCalculatorService.ComputeCart(cartItems);

            _cartService.Set(Cart);
        }
        public IActionResult OnGetPay()
        {
            var cart = _cartService.Get();
            var result = _productQuery.CheckInventoryStatus(cart.CartItems);
            if (result.Any(x => !x.IsInStock))
                return RedirectToPage("Cart");

            var orderId = _orderApplication.PlaceOrder(cart);

            var peymentRespose = _zarinPalFactory.CreatePaymentRequest(cart.PayAmount.ToString(), "09123607930", "aryanteymoori@yahoo.com", "ZarinPal PayMent", orderId);

            return Redirect($"https://{_zarinPalFactory.Prefix}.zarinpal.com/pg/StartPay/{peymentRespose.Authority}");

        }
        public IActionResult OnGetCallBack([FromQuery] string authority, [FromQuery] string status,
            [FromQuery] long oId)
        {
            var orderAmount = _orderApplication.GetAmountBy(oId);
            var verificationResponse = _zarinPalFactory.CreateVerificationRequest(authority, orderAmount.ToString());

            var result = new PaymentResult();
            if (status == "OK" && verificationResponse.Status == 100)
            {
                var issueTrackingNumber = _orderApplication.PaymentSuccedded(oId, verificationResponse.RefID);
                Response.Cookies.Delete("cart-items");
                result = result.Succeeded("پرداخت با موفقیت انجام شد", issueTrackingNumber);
                return RedirectToPage("PaymentResult",result);
            }
            else
            {
                result = result.Failed("پرداخت با مشکل مواجه شده در صورت کسو وجه از حساب ، مبلغ تا 72 ساعت دیگر به حساب شما باز خواهد گشت");
                return RedirectToPage("PaymentResult", result);
            }
        }
    }
}
