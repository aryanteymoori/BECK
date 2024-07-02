using _0_Framework.Infrastructure;
using InventoryManagement.Application.Contract.Inventory;
using InventoryManagement.Configuration.Permissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Inventory
{
    public class IncreaseInventoryModel : PageModel
    {
        public string Message {  get; set; }
        public IncreaseInventory IncreaseInventory { get; set; }
        private readonly IInventoryApplication _inventoryApplication;

        public IncreaseInventoryModel(IInventoryApplication inventoryApplication)
        {
            _inventoryApplication = inventoryApplication;
            IncreaseInventory= new IncreaseInventory();
        }

		[NeedsPermissions(InventoryPermissions.InCreaseInventory)]
		public void OnGet(long id,string message)
        {
            IncreaseInventory.InventoryId = id;
            Message = message;
        }


		[NeedsPermissions(InventoryPermissions.InCreaseInventory)]
		public IActionResult OnPost(IncreaseInventory increaseInventory) 
        {
            var operation=_inventoryApplication.Increase(increaseInventory);
            if (operation.IsSuccedded)
            {
                return RedirectToPage("Index");
            }
            else
            {
                return RedirectToAction("Get", new {increaseInventory.InventoryId,operation.Message});
            }
        }
    }
}
