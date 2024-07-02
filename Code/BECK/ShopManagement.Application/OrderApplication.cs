using _0_Framework.Application;
using Microsoft.Extensions.Configuration;
using ShopManagement.Application.Contract.Order;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Domain.Services;

namespace ShopManagement.Application
{
    public class OrderApplication : IOrderApplication
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IAuthHelper _authHelper;
        private readonly IConfiguration _configuration;
        private readonly IShopInventoryAcl _inventoryAcl;

        public OrderApplication(IOrderRepository orderRepository, IAuthHelper authHelper, IConfiguration configuration, IShopInventoryAcl shopInventoryAcl)
        {
            _orderRepository = orderRepository;
            _authHelper = authHelper;
            _configuration = configuration;
            _inventoryAcl = shopInventoryAcl;
        }


        public long PlaceOrder(Cart cart)
        {
            var accountId = _authHelper.CurrentAccountInfo().Id;

            var order = new Order(accountId, cart.TotalAmount, cart.DiscountAmount, cart.PayAmount);

            foreach (var item in cart.CartItems)
            {
                var orderItem = new OrderItem(item.Id, item.Count, item.DoubleUnitPrice, item.DiscountRate);
                order.AddItem(orderItem);
            }

            _orderRepository.Create(order);
            _orderRepository.SaveChanges();
            return order.Id;
        }

        public string PaymentSuccedded(long orderId, long refId)
        {
            var order = _orderRepository.Get(orderId);
            order.PaymentSuccedded(refId);
            var issueTrackingNumber = CodeGenerator.Generate(_configuration.GetValue<string>("Symbol"));
            order.SetIssueTrackingNumber(issueTrackingNumber);

            if (_inventoryAcl.ReduseFromInventry(order.Items))
            {
                _orderRepository.SaveChanges();
                return issueTrackingNumber;
            }
            return "";

        }

        public double GetAmountBy(long id)
        {
            return _orderRepository.GetAmountBy(id);
        }

        public List<OrderViewModel> Search(OrderSearchModel searchModel)
        {
            return _orderRepository.Search(searchModel);
        }

        public void Cancel(long id)
        {
            var order = _orderRepository.Get(id);
            order.Cancel();
            _orderRepository.SaveChanges();
        }

        public List<OrderItemViewModel> SearchItems(long id)
        {
            return _orderRepository.SearchItems(id);
        }

        public List<OrderState> GetOrdersState()
        {
           return _orderRepository.GetOrdersState();
        }
    }
}
