﻿using _0_Framework.Domain;
using InventoryManagement.Application.Contract.Inventory;

namespace InventoryManagement.Domain.InventoryAgg
{
	public interface IInventoryRepository:IRepository<Inventory,long>
	{
		EditInventory GetDetails(long id);
		Inventory GetBy(long ProductId);
		List<InventoryViewModel> Search(InventorySearchModel searchModel);
        List<InventoryOperationViewModel> GetOperationLog(long inventoryId);
    }
}
