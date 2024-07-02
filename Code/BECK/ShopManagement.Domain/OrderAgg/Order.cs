using _0_Framework.Domain;

namespace ShopManagement.Domain.OrderAgg
{
    public class Order : EntityBase
    {
        public long AccountId { get; private set; }
        public double TotalAmount { get; private set; }
        public double DiscountAmount { get; private set; }
        public double PayAmount { get; private set; }
        public bool IsPaid { get; private set; }
        public bool IsCanceled { get; private set; }
        public string IssueTrackingNumber { get; private set; }
        public long RefId { get; private set; }
        public List<OrderItem> Items { get; private set; }

        public Order(long accountId, double totalAmount, double discountAmount,
            double payAmount)
        {
            AccountId = accountId;
            TotalAmount = totalAmount;
            DiscountAmount = discountAmount;
            PayAmount = payAmount;
            IsPaid = false;
            IsCanceled = false;
            RefId = 0;
            IssueTrackingNumber = "";
            Items = new List<OrderItem>();
        }
        public void PaymentSuccedded(long refId)
        {
            IsPaid = true;
            IsCanceled=false;
            if (refId != 0)
                RefId = refId;
        }
        public void SetIssueTrackingNumber(string issueTrackingNumber)
        {
            IssueTrackingNumber = issueTrackingNumber ?? "";
        }
        public void Cancel()
        {
            IsCanceled = true;
            IsPaid = false;
        }
        public void AddItem(OrderItem orderItems)
        {
            Items.Add(orderItems);
        }
    }
}
