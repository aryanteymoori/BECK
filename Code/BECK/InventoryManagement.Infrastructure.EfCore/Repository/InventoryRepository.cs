using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Infrastructure.EFCore;
using InventoryManagement.Application.Contract.Inventory;
using InventoryManagement.Domain.InventoryAgg;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ShopManagement.Infrastructure.EFCore;

namespace InventoryManagement.Infrastructure.EfCore.Repository
{
    public class InventoryRepository : RepositoryBase<Inventory, long>, IInventoryRepository
    {
        private readonly AccountContext _accountContext;
        private readonly InventoryContext _context;
        private readonly ShopContext _shopContext;

        public InventoryRepository(InventoryContext context, ShopContext shopContext, AccountContext accountContext) : base(context)
        {
            _context = context;
            _shopContext = shopContext;
            _accountContext = accountContext;
        }

        public Inventory GetBy(long ProductId)
        {
            return _context.Inventory.FirstOrDefault(x => x.ProductId == ProductId);
        }

        public EditInventory GetDetails(long id)
        {
            return _context.Inventory.Select(x => new EditInventory
            {
                Id = x.Id,
                ProductId = x.ProductId,
                UnitPrice = x.UnitPrice,
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<InventoryOperationViewModel> GetOperationLog(long inventoryId)
        {
            var accounts = _accountContext.Accounts.Select(x => new { x.Id, x.FullName }).ToList();
            var operations = _context.Inventory.FirstOrDefault(x => x.Id == inventoryId)
                .Operations.Select(x => new InventoryOperationViewModel
                {
                    Id = x.Id,
                    Count = x.Count,
                    CurrentCount = x.CurrentCount,
                    Description = x.Description,
                    Operation = x.Operation,
                    OperationDate = x.OperationDate.ToFarsi(),
                    OperatorId = x.OperatorId,
                    OrderId = x.OrderId,
                }).OrderByDescending(x => x.Id).ToList();

            foreach (var item in operations)
            {
                var Operator = accounts.FirstOrDefault(x => x.Id == item.OperatorId);
                if (Operator != null)
                    item.OperatorName = Operator.FullName;
            }

            return operations;
        }

        public List<InventoryViewModel> Search(InventorySearchModel searchModel)
        {
            var products = _shopContext.Products.Select(x => new { x.Id, x.Name });
            var query = _context.Inventory.Select(x => new InventoryViewModel
            {
                Id = x.Id,
                IsInStock = x.IsInStock,
                ProductId = x.ProductId,
                UnitPrice = x.UnitPrice,
                CreationDate = x.CreateionDate.ToFarsi(),
                CurrentCount = x.CalculateCurrentCount(),
            });
            if (searchModel.ProductId > 0)
                query = query.Where(x => x.ProductId == searchModel.ProductId);

            if (searchModel.IsInStock)
                query = query.Where(x => !x.IsInStock);

            var inventory = query.OrderByDescending(x => x.Id).ToList();
            inventory.ForEach(x => x.ProductName = products.FirstOrDefault(p => p.Id == x.ProductId)?.Name);
            return inventory.ToList();
        }
    }
}
