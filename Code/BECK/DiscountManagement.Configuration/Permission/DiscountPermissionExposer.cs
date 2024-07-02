using _0_Framework.Infrastructure;

namespace DiscountManagement.Configuration.Permission
{
    public class DiscountPermissionExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>
            {
                {
                    "تخفیفات مشتریان",new List<PermissionDto>
                    {
                        new PermissionDto(DiscountPermission.CreateCustomerDiscount,"ایجاد تخفیف مشتری"),
                        new PermissionDto(DiscountPermission.EditCustomerDiscount,"ویرایش تخفیف مشتری"),
                        new PermissionDto(DiscountPermission.ListCustomerDiscount,"گزارش تخفیفات مشتریان"),
                    }
                },
                {
                    "تخفیفات همکاری",new List<PermissionDto>
                    {
                        new PermissionDto(DiscountPermission.CreateColleagueDscount,"ایجاد تخفیف همکاری"),
                        new PermissionDto(DiscountPermission.EditColleagueDiscount,"ویرایش تخفیف همکاری"),
                        new PermissionDto(DiscountPermission.DeleteColleagueDiscount,"حذف تخفیف همکاری"),
                        new PermissionDto(DiscountPermission.RestoreColleagueDiscount,"بازگردانی تخفیف همکاری"),
                        new PermissionDto(DiscountPermission.ListColleagueDiscount,"گزارش تخفیفات همکاری"),
                    }
                },
            };
        }
    }
}
