using _0_Framework.Infrastructure;
using InventoryManagement.Application.Contract.Inventory;
using InventoryManagement.Configuration.Permissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.Inventory
{
    public class OperationLogModel : PageModel
    {
        public List<InventoryOperationViewModel> InventoryOperation {  get; set; }
        private readonly IInventoryApplication _inventoryApplication;

		public OperationLogModel(IInventoryApplication inventoryApplication)
		{
			_inventoryApplication = inventoryApplication;
		}

		[NeedsPermissions(InventoryPermissions.ListProductInventory)]
		public void OnGet(long id)
        {
            InventoryOperation = _inventoryApplication.GetOperationLog(id);
        }
    }
}
