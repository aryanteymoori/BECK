using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ShopManagement.Application.Contract.Slider
{
	public class CreateSlide
	{
		[FileExtentionLimitation(new string[] { ".jpg", ".jpeg", ".png" }, ErrorMessage = "فرمت فایل نادرست است")]
		[MaxFileSize(3 * 1024 * 1024, ErrorMessage = "حداکثر حجم فایل 3 مگابایت میباشد")]
		[MaybeNull()]
		public IFormFile Picture { get; set; }
		[Required(ErrorMessage = "این فیلد نمیتواند خالی باشد")]
		[MaxLength(500,ErrorMessage ="حداکثر 500 کاراکتر")]
		public string PictureAlt { get; set; }
		[Required(ErrorMessage = "این فیلد نمیتواند خالی باشد")]
		[MaxLength(500,ErrorMessage ="حداکثر 500 کاراکتر")]
		public string PictureTitle { get; set; }
		[Required(ErrorMessage = "این فیلد نمیتواند خالی باشد")]
		[MaxLength(255,ErrorMessage ="حداکثر 255 کاراکتر")]
		public string Heading { get; set; }
		[Required(ErrorMessage = "این فیلد نمیتواند خالی باشد")]
		[MaxLength(255,ErrorMessage ="حداکثر 255 کاراکتر")]
		public string Title { get; set; }
		[Required(ErrorMessage = "این فیلد نمیتواند خالی باشد")]
		[MaxLength(255,ErrorMessage ="حداکثر 255 کاراکتر")]
		public string Text { get; set; }
		[Required(ErrorMessage = "این فیلد نمیتواند خالی باشد")]
		[MaxLength(50,ErrorMessage ="حداکثر 50 کاراکتر")]
		public string BtnText { get; set; }
		[Required(ErrorMessage = "این فیلد نمیتواند خالی باشد")]
		public string Link {  get; set; }
    }
}
