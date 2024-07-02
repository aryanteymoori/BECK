using ShopManagement.Domain.OrderAgg;

namespace ShopManagement.Domain.Services
{
    public interface IShopInventoryAcl
    {
        bool ReduseFromInventry(List<OrderItem> orderItems);
    }
}
