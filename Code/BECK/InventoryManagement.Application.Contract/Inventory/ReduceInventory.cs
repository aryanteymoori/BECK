using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Application.Contract.Inventory
{
	public class ReduceInventory 
	{
		public long InventoryId {  get; set; }
		public long ProductId {  get; set; }
		public long Count { get; set; }
		public long OrderId { get; set; }
		[Required(ErrorMessage ="این فیلد نمیتواند خالی باشد")]
		public string Description { get; set; }

        public ReduceInventory()
        {
            
        }

        public ReduceInventory(long productId, long count, long orderId, string description)
        {
            ProductId = productId;
            Count = count;
            OrderId = orderId;
            Description = description;
        }
    }
}
