using System.Diagnostics.CodeAnalysis;

namespace DiscountManagement.Application.Contract.CustomerDiscount
{
    public class CustomerDiscountViewModel
    {
        public long Id {  set; get; }
        public long ProductId {  set; get; }
        public string ProductName { get; set; }
        public int DiscountRate { get; set; }
        public string StartDate { get; set; }
        public DateTime StartDateGr { get; set; }
        public string EndDate { get; set; }
        public DateTime EndDateGr { get; set; }
        public string Reason { get; set; }
    }
}
