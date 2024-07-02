using _0_Framework.Application;
using InventoryManagement.Application.Contract.Inventory;
using InventoryManagement.Domain.InventoryAgg;

namespace InventoryManagement.Application
{
    public class InventoryApplication : IInventoryApplication
	{
		private readonly IInventoryRepository _inventoryRepository;
		private readonly IAuthHelper _authHelper;

        public InventoryApplication(IInventoryRepository inventoryRepository, IAuthHelper authHelper)
        {
            _inventoryRepository = inventoryRepository;
            _authHelper = authHelper;
        }

        public OperationResult Create(CreateInventory command)
		{
			var operation = new OperationResult();
			if (_inventoryRepository.Exist(x => x.ProductId == command.ProductId))
				return operation.Faile("قفسه انبار برای این محصول قبلا ایجاد شده است");
			var inventory = new Inventory(command.ProductId, command.UnitPrice);
			_inventoryRepository.Create(inventory);
			_inventoryRepository.SaveChanges();
			return operation.Success();
		}

		public OperationResult Edit(EditInventory command)
		{
			var operation = new OperationResult();
			var inventory = _inventoryRepository.Get(command.Id);
			if (inventory == null)
				return operation.Faile("رکورد مورد نظر یافت نشد");
			if (_inventoryRepository.Exist(x => x.ProductId == command.ProductId && x.Id != command.Id))
				return operation.Faile("قفسه انبار برای این محصول قبلا ایجاد شده است");
			inventory.Edit(command.ProductId, command.UnitPrice);
			_inventoryRepository.SaveChanges();
			return operation.Success();
		}

		public EditInventory GetDetails(long id)
		{
			return _inventoryRepository.GetDetails(id);
		}

        public List<InventoryOperationViewModel> GetOperationLog(long inventoryId)
        {
            return _inventoryRepository.GetOperationLog(inventoryId);
        }

        public OperationResult Increase(IncreaseInventory command)
		{
			var operation = new OperationResult();
			var inventory = _inventoryRepository.Get(command.InventoryId);
			if (inventory == null)
				return operation.Faile("رکورد مورد نظر یافت نشد");
			inventory.Increase(command.Count, 1, command.Description);
			_inventoryRepository.SaveChanges();
			return operation.Success();
		}

		public OperationResult Reduce(List<ReduceInventory> command)
		{
			var operation = new OperationResult();
			foreach (var item in command)
			{
				var inventory = _inventoryRepository.GetBy(item.ProductId);
				if (inventory == null)
					return operation.Faile("رکورد مورد نظر یافت نشد");
				inventory.Reduce(item.Count, _authHelper.CurrentAccountId(), item.Description,item.OrderId);
			}
			_inventoryRepository.SaveChanges();
			return operation.Success();
		}

		public OperationResult Reduce(ReduceInventory command)
		{
			var operation = new OperationResult();
			var inventory = _inventoryRepository.Get(command.InventoryId);
			if (inventory == null)
				return operation.Faile("رکورد مورد نظر یافت نشد");
			inventory.Reduce(command.Count, _authHelper.CurrentAccountId(), command.Description, 0);
			_inventoryRepository.SaveChanges();
			return operation.Success();
		}

		public List<InventoryViewModel> Search(InventorySearchModel searchModel)
		{
			return _inventoryRepository.Search(searchModel);
		}
	}
}
