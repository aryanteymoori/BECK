using _0_Framework.Infrastructure;
using InventoryManagement.Application.Contract.Inventory;
using InventoryManagement.Configuration.Permissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.Inventory
{
    public class ReduceInventoryModel : PageModel
    {
        public string Message {  get; set; }
        public ReduceInventory ReduceInventory { get; set; }
        private readonly IInventoryApplication _inventoryApplication;

        public ReduceInventoryModel(IInventoryApplication inventoryApplication)
        {
            _inventoryApplication = inventoryApplication;
            ReduceInventory = new ReduceInventory();
        }

		[NeedsPermissions(InventoryPermissions.ReduseInventory)]
		public void OnGet(long id,string message)
        {
            ReduceInventory.InventoryId = id;
            Message = message;
        }


		[NeedsPermissions(InventoryPermissions.ReduseInventory)]
		public IActionResult OnPost(ReduceInventory reduceInventory) 
        {
            var operation=_inventoryApplication.Reduce(reduceInventory);
            if (operation.IsSuccedded)
            {
                return RedirectToPage("Index");
            }
            else
            {
                return RedirectToAction("Get", new { reduceInventory.InventoryId, operation.Message });
            }
        }
    }
}
