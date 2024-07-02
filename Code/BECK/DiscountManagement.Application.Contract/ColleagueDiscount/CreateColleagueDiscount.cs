using System.ComponentModel.DataAnnotations;

namespace DiscountManagement.Application.Contract.ColleagueDiscount
{
    public class CreateColleagueDiscount
    {
        public long ProductId { get; set; }
        [Required(ErrorMessage ="این فیلد نمیتواند خالی باشد")]
        public int DiscountRate { get; set; }
    }

}
