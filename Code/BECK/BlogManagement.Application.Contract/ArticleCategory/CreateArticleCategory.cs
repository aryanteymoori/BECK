using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;


namespace BlogManagement.Application.Contract.ArticleCategory
{
    public class CreateArticleCategory
    {
        [Required(ErrorMessage = "این فیلد نمیتواندخالی باشد")]
        public string Name { get; set; }

        [FileExtentionLimitation(new string[] { ".jpg", ".jpeg", ".png" })]
        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = "حداکثر حجم فایل 3 مگابایت میباشد")]
        [MaybeNull()]
        public IFormFile Picture { get; set; }


        //[MaybeNull()]
        [Required(ErrorMessage = "این فیلد نمیتواندخالی باشد")]
        public string Description { get; set; }


		//[MaybeNull()]
		public int ShowOrder { get; set; }


        [Required(ErrorMessage = "این فیلد نمیتواندخالی باشد")]
        public string Slug { get; set; }


        //[MaybeNull()]
        [Required(ErrorMessage = "این فیلد نمیتواندخالی باشد")]
        public string Keywords { get; set; }


		//[MaybeNull()]
		[Required(ErrorMessage = "این فیلد نمیتواندخالی باشد")]
        public string MetaDescription { get; set; }


		[MaybeNull()]
        public string CanonicalAddress { get; set; }
    }
}
