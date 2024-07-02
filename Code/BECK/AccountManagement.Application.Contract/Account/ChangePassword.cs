using System.ComponentModel.DataAnnotations;

namespace AccountManagement.Application.Contract.Account
{
    public class ChangePassword
    {
        public long Id { get; set; }
        [Required(ErrorMessage ="این فیلد نمیتواند خالی باشد")]
        [MinLength(8,ErrorMessage ="کلمه عبور حداقل بای 8 کاراکتر باشد")]
        public string Password {  get; set; }
		[Required(ErrorMessage ="این فیلد نمیتواند خالی باشد")]
        [MinLength(8,ErrorMessage ="کلمه عبور حداقل بای 8 کاراکتر باشد")]
		public string RePassword {  get; set; }
	}
}
