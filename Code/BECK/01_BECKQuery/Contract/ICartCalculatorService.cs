using InventoryManagement.Infrastructure.EfCore;
using ShopManagement.Application.Contract.Order;
using ShopManagement.Infrastructure.EFCore;

namespace _01_BECKQuery.Contract
{
    public interface ICartCalculatorService
    {
        Cart ComputeCart(List<CartItem> cartItems);
    }
}
