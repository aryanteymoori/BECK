using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contract.Order;
using ShopManagement.Domain.OrderAgg;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
	public class OrderRepository : RepositoryBase<Order, long>, IOrderRepository
	{
		private readonly ShopContext _context;
		private readonly AccountContext _accountContext;
		public OrderRepository(ShopContext context, AccountContext accountContext) : base(context)
		{
			_context = context;
			_accountContext = accountContext;
		}

		public double GetAmountBy(long id)
		{
			var order = _context.Orders.Select(x => new { x.Id, x.PayAmount }).FirstOrDefault(x => x.Id == id);
			if (order != null)
				return order.PayAmount;
			return 0;
		}

        public List<OrderState> GetOrdersState()
        {
            return _context.Orders.Select(x => new OrderState 
			{
				Id = x.Id,
				IsCanceled = x.IsCanceled,
				IsPaid=x.IsPaid,
			}).ToList();
        }

        public List<OrderViewModel> Search(OrderSearchModel searchModel)
		{
			var accounts = _accountContext.Accounts.Select(x => new { x.Id, x.FullName }).ToList();
			var query = _context.Orders.Select(x => new OrderViewModel
			{
				Id = x.Id,
				AccountId = x.AccountId,
				DiscountAmount = x.DiscountAmount,
				IsCanceled = x.IsCanceled,
				IsPaid = x.IsPaid,
				IssueTrackingNumber = x.IssueTrackingNumber??"",
				RefId = x.RefId,
				PayAmount = x.PayAmount,
				TotalAmount = x.TotalAmount,
				CreationDate = x.CreateionDate.ToFarsi()
			});

			if (searchModel.AccountId > 0)
				query = query.Where(x => x.AccountId == searchModel.AccountId);

			if (searchModel.IsCanceled)
				query = query.Where(x => x.IsCanceled);

			var orders = query.OrderByDescending(x => x.Id).ToList();

			foreach (var item in orders)
			{
				item.AccountName = accounts.FirstOrDefault(x => x.Id == item.AccountId)?.FullName;
			}

			return orders;
		}

		public List<OrderItemViewModel> SearchItems(long id)
		{
			var products = _context.Products.Select(x => new { x.Id, x.Name }).ToList();
			var order = _context.Orders.FirstOrDefault(x => x.Id == id);
			if (order == null)
				return new List<OrderItemViewModel>();
			var items = order.Items.Select(x => new OrderItemViewModel
			{
				Id = x.Id,
				Count = x.Count,
				UnitPrice = x.UnitPrice,
				OrderId = x.OrderId,
				DiscountRate = x.DiscountRate,
				ProductId = x.ProductId,
			}).ToList();

			items.ForEach(x => x.ProductName = products.FirstOrDefault(z => z.Id == x.ProductId).Name);

			return items.OrderByDescending(x => x.OrderId).ToList();
		}
	}
}
