using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ShopManagement.Application.Contract.ProductPicture
{
	public class CreateProductPicture
	{

		[Required(ErrorMessage ="این فیلد نمیتواند خالی باشد")]
		public long ProductId { get; set; }
		[FileExtentionLimitation(new string[] { ".jpg", ".jpeg", ".png" }, ErrorMessage = "فرمت فایل نادرست است")]
		[MaxFileSize(3 * 1024 * 1024, ErrorMessage = "حداکثر حجم فایل 3 مگابایت میباشد")]
		[MaybeNull()]
		public IFormFile Picture { get; set; }
		[Required(ErrorMessage ="این فیلد نمیتواند خالی باشد")]
		[MaxLength(500,ErrorMessage ="حداکثر 500 کاراکتر")]
		public string PictureAlt { get; set; }
		[Required(ErrorMessage ="این فیلد نمیتواند خالی باشد")]
		[MaxLength(500,ErrorMessage ="حداکثر 500 کاراکتر")]
		public string PictureTitle { get; set; }
	}

}
