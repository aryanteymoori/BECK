using InventoryManagement.Application.Contract.Inventory;

namespace _01_BECKQuery.Contract.Inventory
{
    public interface IInventoryQuery
    {
        StockStatus CheckStock(IsInStock command);
    }
}
