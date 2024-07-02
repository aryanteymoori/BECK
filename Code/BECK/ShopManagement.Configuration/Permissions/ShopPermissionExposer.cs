using _0_Framework.Infrastructure;

namespace ShopManagement.Configuration.Permissions
{
    public class ShopPermissionExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>
            {
                {
                    "بخش محصولات",new List<PermissionDto>
                    {
                        new PermissionDto(ShopPermissions.CreateProduct,"ایجاد محصول جدید"),
                        new PermissionDto(ShopPermissions.EditProduct,"ویرایش محصولات"),
                        new PermissionDto(ShopPermissions.ListProduct,"گزارش محصولات"),
                    }
                },
                {
                    "بخش دسته بندی محصولات",new List<PermissionDto>
                    {
                        new PermissionDto(ShopPermissions.CreateProductCategory,"ایجاد دسته محصول جدید"),
                        new PermissionDto(ShopPermissions.EditProductCategory,"ویرایش دسته بندی محصولات"),
                        new PermissionDto(ShopPermissions.ListProductCategory,"گزارش دسته بندی محصولات"),
                    }
                },
                {
                    "بخش عکس محصولات",new List<PermissionDto>
                    {
                        new PermissionDto(ShopPermissions.CreateProductPicture,"ایجاد عکس محصول جدید"),
                        new PermissionDto(ShopPermissions.EditProductPicture,"ویرایش عکس محصول"),
                        new PermissionDto(ShopPermissions.DeleteProductPicture,"حذف عکس محصول"),
                        new PermissionDto(ShopPermissions.RestoreProductPicture,"بازگردانی عکس محصول"),
                        new PermissionDto(ShopPermissions.ListProductPicture,"گزارش عکس محصولات"),
                    }
                },
                 {
                    "بخش اسلایدر",new List<PermissionDto>
                    {
                        new PermissionDto(ShopPermissions.CreateSlider,"ایجاد اسلایدر جدید"),
                        new PermissionDto(ShopPermissions.EditSlider,"ویرایش اسلایدر"),
                        new PermissionDto(ShopPermissions.DeleteSlider,"حذف اسلایدر"),
                        new PermissionDto(ShopPermissions.Restoreslider,"بازگردانی اسلایدر"),
                        new PermissionDto(ShopPermissions.ListSlider,"گزارش اسلایدر ها"),
                    }
                },
            };
        }
    }
}
