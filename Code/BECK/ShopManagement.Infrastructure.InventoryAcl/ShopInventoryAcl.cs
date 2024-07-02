﻿using InventoryManagement.Application.Contract.Inventory;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Domain.Services;

namespace ShopManagement.Infrastructure.InventoryAcl
{
    public class ShopInventoryAcl : IShopInventoryAcl
    {
        private readonly IInventoryApplication _inventoryApplication;

        public ShopInventoryAcl(IInventoryApplication inventoryApplication)
        {
            _inventoryApplication = inventoryApplication;
        }

        public bool ReduseFromInventry(List<OrderItem> orderItems)
        {
            var command = new List<ReduceInventory>();
            foreach(var orderItem in orderItems)
            {
                var item=new ReduceInventory(orderItem.ProductId,orderItem.Count,orderItem.OrderId,"خرید مشتری");
                command.Add(item);
            }

            return _inventoryApplication.Reduce(command).IsSuccedded;
        }
    }
}
