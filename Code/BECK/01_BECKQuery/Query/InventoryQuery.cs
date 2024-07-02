using _01_BECKQuery.Contract.Inventory;
using InventoryManagement.Application.Contract.Inventory;
using InventoryManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EFCore;

namespace _01_BECKQuery.Query
{
    public class InventoryQuery : IInventoryQuery
    {
        private readonly InventoryContext _inventoryContext;
        private readonly ShopContext _shopContext;

        public InventoryQuery(InventoryContext inventoryContext, ShopContext shopContext)
        {
            _inventoryContext = inventoryContext;
            _shopContext = shopContext;
        }

        public StockStatus CheckStock(IsInStock command)
        {
            var inventory = _inventoryContext.Inventory.Include(x=>x.Operations).FirstOrDefault(x => x.ProductId == command.ProductId);
            if (inventory == null || !(inventory.CalculateCurrentCount() >= command.Count))
            {
                    var product = _shopContext.Products.Select(x => new { x.Name, x.Id }).FirstOrDefault(x => x.Id == command.ProductId);
                    if (product != null)
                    {
                        return new StockStatus()
                        {
                            IsStock = false,
                            ProductName = product.Name
                        };
                    }
            }
            return new StockStatus() { IsStock = true };
        }
    }
}
