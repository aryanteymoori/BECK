using _0_Framework.Infrastructure;
using InventoryManagement.Application.Contract.Inventory;
using InventoryManagement.Configuration.Permissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contract.Product;

namespace ServiceHost.Areas.Administration.Pages.Inventory
{
    public class EditInventoryModel : PageModel
    {
        public string Message { get; set; }
        public SelectList Products { get; set; }
        public EditInventory EditInventory { get; set; }
        private readonly IInventoryApplication _inventoryApplication;
        private readonly IProductApplication _productApplication;

        public EditInventoryModel(IInventoryApplication inventoryApplication, IProductApplication productApplication)
        {
            _inventoryApplication = inventoryApplication;
            _productApplication = productApplication;
        }

		[NeedsPermissions(InventoryPermissions.EditInventory)]
		public void OnGet(long id,string message)
        {
            EditInventory=_inventoryApplication.GetDetails(id);
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            Message = message;
        }

		[NeedsPermissions(InventoryPermissions.EditInventory)]
		public IActionResult OnPost(EditInventory editInventory)
        {
            var operation = _inventoryApplication.Edit(editInventory);
            if (operation.IsSuccedded)
            {
                return RedirectToPage("Index");
            }
            else
            {
                return RedirectToAction("Get", new {editInventory.Id, operation.Message });
            }
        }
    }
}
