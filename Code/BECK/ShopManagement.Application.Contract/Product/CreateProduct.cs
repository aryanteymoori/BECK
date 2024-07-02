using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ShopManagement.Application.Contract.Product
{
	public class CreateProduct
	{
		[Required(ErrorMessage ="این فیلد نمیتواند خالی باشد")]
		[MaxLength(255,ErrorMessage ="حداکثر 255 کاراکتر")]
		public string Name { get; set; }
		[Required(ErrorMessage ="این فیلد نمیتواند خالی باشد")]
		[MaxLength(15,ErrorMessage ="حداکثر 15 کاراکتر")]
		public string Code { get; set; }
		[Required(ErrorMessage ="این فیلد نمیتواند خالی باشد")]
		[MaxLength(500,ErrorMessage ="حداکثر 500 کاراکتر")]
		public string shortDescription { get; set; }
		[MaybeNull()]
		public string Description { get; set; }

		[FileExtentionLimitation(new string[] { ".jpg", ".jpeg", ".png" }, ErrorMessage = "فرمت فایل نادرست است")]
		[MaxFileSize(3 * 1024 * 1024, ErrorMessage = "حداکثر حجم فایل 3 مگابایت میباشد")]
		[MaybeNull()]
		public IFormFile Picture { get; set; }
		[MaxLength(255,ErrorMessage ="حداکثر 255 کاراکتر")]
		[Required(ErrorMessage ="این فیلد نمیتواند خالی باشد")]
		public string PictureAlt { get; set; }
		[MaxLength(500,ErrorMessage ="حداکثر 500 کاراکتر")]
		[Required(ErrorMessage ="این فیلد نمیتواند خالی باشد")]
		public string PictureTitle { get; set; }
		public long CategoryId { get; set; }
		[Required(ErrorMessage ="این فیلد نمیتواند خالی باشد")]
		[MaxLength(500,ErrorMessage ="حداکثر 500 کاراکتر")]
		public string Slug { get; set; }
		[Required(ErrorMessage ="این فیلد نمیتواند خالی باشد")]
		[MaxLength(100,ErrorMessage ="حداکثر 100 کاراکتر")]
		public string Keywords { get; set; }
		[Required(ErrorMessage ="این فیلد نمیتواند خالی باشد")]
		[MaxLength(150,ErrorMessage ="حداکثر 150 کاراکتر")]
		public string MetaDescription { get; set; }
	}
}
