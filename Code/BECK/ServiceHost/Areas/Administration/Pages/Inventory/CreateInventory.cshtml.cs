using _0_Framework.Infrastructure;
using InventoryManagement.Application.Contract.Inventory;
using InventoryManagement.Configuration.Permissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contract.Product;

namespace ServiceHost.Areas.Administration.Pages.Inventory
{
    public class CreateInventoryModel : PageModel
    {
        public string Message {  get; set; }
        public SelectList Products { get; set; }
        public CreateInventory CreateInventory { get; set; }
        private readonly IInventoryApplication _inventoryApplication;
        private readonly IProductApplication _productApplication;

		public CreateInventoryModel(IInventoryApplication inventoryApplication, IProductApplication productApplication)
		{
			_inventoryApplication = inventoryApplication;
			_productApplication = productApplication;
		}

		[NeedsPermissions(InventoryPermissions.CreateInventory)]
		public void OnGet(string message)
        {
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            Message = message;
        }

		[NeedsPermissions(InventoryPermissions.CreateInventory)]
		public IActionResult OnPost(CreateInventory createInventory)
        {
            var operation = _inventoryApplication.Create(createInventory);
            if (operation.IsSuccedded)
            {
                return RedirectToPage("Index");
            }
            else
            {
                return RedirectToAction("Get", new { operation.Message });
            }
        }
    }
}
