namespace ShopManagement.Application.Contract.Order
{
    public class OrderState
    {
        public long Id { get; set; }
        public bool IsPaid { get; set; }
        public bool IsCanceled { get; set; }
    }
}
