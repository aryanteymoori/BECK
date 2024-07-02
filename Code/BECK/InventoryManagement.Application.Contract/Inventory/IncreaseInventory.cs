using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace InventoryManagement.Application.Contract.Inventory
{
	public class IncreaseInventory
	{
		public long InventoryId {  get; set; }
		public long Count {  get; set; }
		[Required(ErrorMessage ="این فیلد نمیتواند خالی باشد")]
		public string Description {  get; set; }
	}
}
