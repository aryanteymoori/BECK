using _0_Framework.Application;

namespace ShopManagement.Application.Contract.Order
{
    public interface IOrderApplication
    {
        long PlaceOrder(Cart cart);
        string PaymentSuccedded(long orderId, long refId);
        void Cancel(long id);
        double GetAmountBy(long id);
        List<OrderViewModel> Search(OrderSearchModel searchModel);
        List<OrderItemViewModel> SearchItems(long id);
        List<OrderState> GetOrdersState();
    }
}
