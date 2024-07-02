using System.ComponentModel.DataAnnotations;

namespace AccountManagement.Application.Contract.Account
{
    public class Login
    {
        [Required(ErrorMessage ="این فیلد نمیتواند خالی باشد")]
        public string UserName { get; set; }
        [Required(ErrorMessage ="این فیلد نمیتواند خالی باشد")]
        [MinLength(8,ErrorMessage ="کلمه عبور باید حداقل 8 کاراکتر باشد")]
        public string Password { get; set; }
    }
}
