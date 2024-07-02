using _0_Framework.Infrastructure;

namespace InventoryManagement.Configuration.Permissions
{
	public class InventoryPermissionExposer : IPermissionExposer
	{
		public Dictionary<string, List<PermissionDto>> Expose()
		{
			return new Dictionary<string, List<PermissionDto>> 
			{
				{
					"بخش انبار داری",new List<PermissionDto> 
					{
						new PermissionDto(InventoryPermissions.CreateInventory,"ایجاد انبار جدید"),
						new PermissionDto(InventoryPermissions.EditInventory,"ویرایش انبار ها"),
						new PermissionDto(InventoryPermissions.InCreaseInventory,"افزودن موجودی محصول"),
						new PermissionDto(InventoryPermissions.ReduseInventory,"کاهش موجودی محصول"),
						new PermissionDto(InventoryPermissions.ListInventory,"گزارش انبار ها"),
						new PermissionDto(InventoryPermissions.ListProductInventory,"گزارش گردش انبار ها"),
					}
				}
			};
		}
	}
}
