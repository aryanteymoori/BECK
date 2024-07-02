namespace DiscountManagement.Application.Contract.CustomerDiscount
{
    public class CustomerDiscountSearchModel
    {
        public long ProductId { set; get; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
