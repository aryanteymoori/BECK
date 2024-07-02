using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopManagement.Application.Contract.Order;

namespace ServiceHost.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChangeOrderStateController : ControllerBase
    {
        private readonly IOrderApplication _orderApplication;

        public ChangeOrderStateController(IOrderApplication orderApplication)
        {
            _orderApplication = orderApplication;
        }

        [HttpPost("Paid/{id}")]
        public List<OrderState> Paid(long id)
        {
            _orderApplication.PaymentSuccedded(id,0);
            return _orderApplication.GetOrdersState();
        }

        [HttpPost("Canceled/{id}")]
        public List<OrderState> Canceled(long id)
        {
            _orderApplication.Cancel(id);
            return _orderApplication.GetOrdersState();
        }
    }
}
