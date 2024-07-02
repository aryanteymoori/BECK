using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Application.Contract.Inventory
{
	public class CreateInventory
	{
		[Required(ErrorMessage ="این فیلد نمیتواند خالی باشد")]
		public long ProductId { get; set; }
		[Required(ErrorMessage ="این فیلد نمیتواند خالی باشد")]
		public double UnitPrice { get; set; }
    }
}
