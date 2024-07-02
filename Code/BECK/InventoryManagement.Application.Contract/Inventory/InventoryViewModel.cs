namespace InventoryManagement.Application.Contract.Inventory
{
	public class InventoryViewModel
	{
		public long Id {  set; get; }
		public long CurrentCount { get; set; }
		public long ProductId {  set; get; }
		public string ProductName {  get; set; }
		public bool IsInStock {  get; set; }
		public double UnitPrice {  get; set; }
		public string CreationDate { get; set; }
	}
}
