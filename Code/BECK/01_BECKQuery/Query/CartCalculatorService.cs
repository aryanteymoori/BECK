using _0_Framework.Application;
using _01_BECKQuery.Contract;
using DiscountManagement.Infrastructure.EFCore;
using ShopManagement.Application.Contract.Order;

namespace _01_BECKQuery.Query
{
    public class CartCalculatorService : ICartCalculatorService
    {
        //private readonly ShopContext _shopContext;
        //private readonly InventoryContext _inventoryContext;
        private readonly DiscountContext _discountContext;
        private readonly IAuthHelper _authHelper;

        public CartCalculatorService(DiscountContext discountContext, IAuthHelper authHelper)
        {
            _discountContext = discountContext;
            _authHelper = authHelper;
        }

        public Cart ComputeCart(List<CartItem> cartItems)
        {
            var cart = new Cart();
            var colleagueDiscounts = _discountContext.ColleagueDiscounts
                .Where(x => !x.IsDeleted)
                .Select(x => new { x.DiscountRate, x.ProductId })
                .ToList();

            var customerDiscounts = _discountContext.CustomerDiscounts
                .Where(x => x.EndDate > DateTime.Now && x.StartDate < DateTime.Now)
                .Select(x => new { x.DiscountRate, x.ProductId })
                .ToList();
            var currentAccountRole = _authHelper.CurrentAccountRole();

            if (cartItems != null)
            {
                foreach (var item in cartItems)
                {
                    if (currentAccountRole == Roles.ColleagueUser)
                    {
                        var colleagueDiscount = colleagueDiscounts.FirstOrDefault(x => x.ProductId == item.Id);
                        if (colleagueDiscounts != null)
                            item.DiscountRate = colleagueDiscount.DiscountRate;
                    }
                    else
                    {
                        var customerDiscount = customerDiscounts.FirstOrDefault(x => x.ProductId == item.Id);
                        if (customerDiscount != null)
                            item.DiscountRate = customerDiscount.DiscountRate;
                    }
                    item.DiscountAmount = ((item.DiscountRate * (item.DoubleUnitPrice * item.Count)) / 100);
                    item.ItemPayAmount = (item.DoubleUnitPrice * item.Count) - item.DiscountAmount;
                    cart.Add(item);
                }
            }

            //if (currentAccountRole == Roles.ColleagueUser)
            //{
            //    foreach (var item in cartItems)
            //    {
            //        var colleagueDiscount = colleagueDiscounts.FirstOrDefault(x => x.ProductId == item.Id);
            //        if (colleagueDiscounts == null) continue;
            //        item.DiscountRate = colleagueDiscount.DiscountRate;
            //        item.DiscountAmount = ((item.DiscountRate * (item.DoubleUnitPrice * item.Count)) / 100);
            //        item.ItemPayAmount = (item.DoubleUnitPrice * item.Count) - item.DiscountAmount;
            //        cart.Add(item);
            //    }
            //}
            //else
            //{
            //    foreach (var item in cartItems)
            //    {
            //        var customerDiscount = customerDiscounts.FirstOrDefault(x => x.ProductId == item.Id);
            //        if (colleagueDiscounts == null) continue;
            //        item.DiscountRate = customerDiscount.DiscountRate;
            //        item.DiscountAmount = ((item.DiscountRate * (item.DoubleUnitPrice * item.Count)) / 100);
            //        item.ItemPayAmount = (item.DoubleUnitPrice * item.Count) - item.DiscountAmount;
            //        cart.Add(item);
            //    }
            //}
            return cart;
        }
    }
}
