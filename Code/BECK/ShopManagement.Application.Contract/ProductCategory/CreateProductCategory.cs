using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ShopManagement.Application.Contract.ProductCategory
{
	public class CreateProductCategory
	{
		[Required(ErrorMessage = "این فیلد نمیتواند خالی باشد")]
		public string Name { get; set; }
		public string Description { get; set; }

		[FileExtentionLimitation(new string[] {".jpg",".jpeg",".png" },ErrorMessage ="فرمت فایل نادرست است")]
		[MaxFileSize(3 * 1024 * 1024, ErrorMessage = "حداکثر حجم فایل 3 مگابایت میباشد")]
		//[Required(ErrorMessage = "این فیلد نمیتواند خالی باشد")]
		[MaybeNull()]
		public IFormFile Picture { get; set; }
		public string PictureAlt { get; set; }
		public string PictureTitle { get; set; }
		public string Keywords { get; set; }
		[Required(ErrorMessage = "این فیلد نمیتواند خالی باشد")]
		public string MetaDescription { get; set; }
		public string Slug { get; set; }
	}
}
