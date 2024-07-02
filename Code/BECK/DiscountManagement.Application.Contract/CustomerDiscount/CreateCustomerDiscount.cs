using System.ComponentModel.DataAnnotations;

namespace DiscountManagement.Application.Contract.CustomerDiscount
{
    public class CreateCustomerDiscount
    {
        [Required(ErrorMessage = "این فیلد نمیتواند خالی باشد")]
        public long ProductId { get; set; }
        [Required(ErrorMessage = "این فیلد نمیتواند خالی باشد")]
        public int DiscountRate { get; set; }
		public string StartDate { get; set; }
        public string EndDate { get; set; }
        [Required(ErrorMessage = "این فیلد نمیتواند خالی باشد")]
        public string Reason { get; set; }
    }
}
