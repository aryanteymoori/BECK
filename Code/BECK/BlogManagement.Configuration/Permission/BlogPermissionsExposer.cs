using _0_Framework.Infrastructure;

namespace BlogManagement.Configuration.Permission
{
    public class BlogPermissionsExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>
            {
                {
                    "بخش دسته بندی مقالات",new List<PermissionDto>
                    {
                        new PermissionDto(BlogPermission.CreateArticleCategory,"ایجاد دسته بندی مقاله"),
                        new PermissionDto(BlogPermission.EditArticleCategory,"ویرایش دسته بندی مقاله"),
                        new PermissionDto(BlogPermission.DeleteArticleCategory,"حذف دسته بندی مقاله"),
                        new PermissionDto(BlogPermission.RestoreArticleCategory,"بازگردانی دسته بندی مقاله"),
                        new PermissionDto(BlogPermission.ListArticleCategory,"گزارش دسته بندی مقالات")
                    }
                },
                {
                    "بخش مقالات",new List<PermissionDto>
                    {
                        new PermissionDto(BlogPermission.CreateArticle,"ایجاد مقاله"),
                        new PermissionDto(BlogPermission.EditArticle,"ویرایش مقاله"),
                        new PermissionDto(BlogPermission.DeleteArticle,"حذف مقاله"),
                        new PermissionDto(BlogPermission.RestoreArticle,"بازگردانی مقاله"),
                        new PermissionDto(BlogPermission.ListArticle,"گزارش  مقالات")
                    }
                }
            };
        }
    }
}
