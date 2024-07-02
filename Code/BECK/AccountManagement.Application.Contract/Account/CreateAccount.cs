using System.ComponentModel.DataAnnotations;

namespace AccountManagement.Application.Contract.Account
{
    public class CreateAccount
    {
        [Required(ErrorMessage = "این فیلد نمیتواند خالی باشد")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "این فیلد نمیتواند خالی باشد")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "این فیلد نمیتواند خالی باشد")]
        [MinLength(8, ErrorMessage = "کلمه عبور حداقل بای 8 کاراکتر باشد")]
        public string Password { get; set; }
        [Required(ErrorMessage = "این فیلد نمیتواند خالی باشد")]
        [MinLength(11, ErrorMessage = "شماره موبایل صحیح نیست")]
        [MaxLength(11, ErrorMessage = "شماره موبایل صحیح نیست")]
        public string Mobile { get; set; }
        public long RoleId { get; set; }
    }
}
