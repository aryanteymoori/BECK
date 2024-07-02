using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BlogManagement.Application.Contract.Article
{
	public class CreateArticle
	{
		[Required(ErrorMessage ="این فیلد نمیتواند خالی باشد")]
		public string Title { get; set; }


		[Required(ErrorMessage ="این فیلد نمیتواند خالی باشد")]
		public string ShortDescription { get; set; }


		[Required(ErrorMessage ="این فیلد نمیتواند خالی باشد")]
		public string Description { get; set; }

		[FileExtentionLimitation(new string[] { ".jpg", ".jpeg", ".png" })]
		[MaxFileSize(3*1024*1024,ErrorMessage ="حداکثر حجم فایل 3 مگابایت میباشد")]
		[MaybeNull()]
		public IFormFile Picture { get; set; }


		[MaybeNull()]
		public string PictureAlt { get; set; }


		[MaybeNull()]
		public string PictureTitle { get; set; }


		[Required(ErrorMessage ="این فیلد نمیتواند خالی باشد")]
		public string Slug { get; set; }


		[MaybeNull()]
		public string Keywords { get; set; }


		[MaybeNull()]
		public string MetaDescription { get; set; }


		[MaybeNull()]
		public string CanonicalAddress { get; set; }


		[Required(ErrorMessage ="این فیلد نمیتواند خالی باشد")]
		public long CategoryId { get; set; }
	}
}
