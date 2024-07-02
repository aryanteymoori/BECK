using _0_Framework.Application;
using _0_Framework.Infrastructure;
using InventoryManagement.Application.Contract.Inventory;
using InventoryManagement.Configuration.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.Inventory
{
    public class IndexModel : PageModel
    {
        public List<InventoryViewModel> Inventory { get; set; }
        private readonly IInventoryApplication _inventoryApplication;

		public IndexModel(IInventoryApplication inventoryApplication)
		{
			_inventoryApplication = inventoryApplication;
		}

        [NeedsPermissions(InventoryPermissions.ListInventory)]
		public void OnGet(InventorySearchModel searchModel)
        {
            Inventory = _inventoryApplication.Search(searchModel);
        }

		[NeedsPermissions(InventoryPermissions.InCreaseInventory)]
		public IActionResult OnGetIncrease(long id)
        {
            return RedirectToPage("IncreaseInventory",new {id});
        }

		[NeedsPermissions(InventoryPermissions.ReduseInventory)]
		public IActionResult OnGetReduce(long id)
        {
            return RedirectToPage("ReduceInventory", new { id });
        }

		[NeedsPermissions(InventoryPermissions.ListProductInventory)]
		public IActionResult OnGetOperationLog(long id)
        {
            return RedirectToPage("OperationLog", new { id });
        }

    }
}
