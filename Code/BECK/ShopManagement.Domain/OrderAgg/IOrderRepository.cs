using _0_Framework.Domain;
using ShopManagement.Application.Contract.Order;

namespace ShopManagement.Domain.OrderAgg
{
    public interface IOrderRepository:IRepository<Order,long>
    {
        double GetAmountBy(long id);
        List<OrderViewModel> Search(OrderSearchModel searchModel);
        List<OrderItemViewModel> SearchItems(long id);
        List<OrderState> GetOrdersState();
    }
}
