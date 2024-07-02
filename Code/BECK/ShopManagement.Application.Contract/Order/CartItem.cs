namespace ShopManagement.Application.Contract.Order
{
    public class CartItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string UnitPrice {  get; set; }
        public double DoubleUnitPrice {  get; set; }
        public string Picture {  get; set; }
        public int Count {  get; set; }
        public bool IsInStock {  get; set; }
        public int DiscountRate {  get; set; }
        public double DiscountAmount { get; set; }
        public double ItemPayAmount { get; set; }

    }
}
