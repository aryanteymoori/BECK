using _0_Framework.Domain;

namespace InventoryManagement.Domain.InventoryAgg
{
	public class Inventory : EntityBase
	{
		public long ProductId { get; private set; }
		public double UnitPrice { get; private set; }
		public bool IsInStock { get; private set; }
		public List<InventoryOperation> Operations { get; private set; }

		public Inventory(long productId, double unitPrice)
		{
			ProductId = productId;
			UnitPrice = unitPrice;
			IsInStock = false;
			Operations = new List<InventoryOperation>();
		}
		public void Edit(long productId, double unitPrice)
		{
			ProductId = productId;
			UnitPrice = unitPrice;
			IsInStock = false;
		}
		public long CalculateCurrentCount()
		{
			var plus = Operations.Where(x => x.Operation).Sum(x => x.Count);
			var mines = Operations.Where(x => !x.Operation).Sum(x => x.Count);
			return plus - mines;
		}
		public void Increase(long count, long operatorId, string description)
		{
			var currentCount = CalculateCurrentCount() + count;
			var operation = new InventoryOperation(true, count, operatorId, currentCount, description, 0, Id);
			Operations.Add(operation);
			IsInStock = currentCount > 0;
		}
		public void Reduce(long count, long operatorId, string description, long orderId)
		{
			var currentCount = CalculateCurrentCount() - count;
			var operation = new InventoryOperation(false, count, operatorId, currentCount, description, orderId, Id);
			Operations.Add(operation);
			IsInStock = currentCount > 0;
		}
	}
}
