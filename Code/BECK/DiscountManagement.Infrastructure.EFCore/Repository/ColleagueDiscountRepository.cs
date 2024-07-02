using _0_Framework.Application;
using _0_Framework.Infrastructure;
using DiscountManagement.Application.Contract.ColleagueDiscount;
using DiscountManagement.Domain.ColleagueDiscountAgg;
using ShopManagement.Infrastructure.EFCore;
using System.Linq.Expressions;
using System.Runtime.InteropServices;

namespace DiscountManagement.Infrastructure.EFCore.Repository
{
    public class ColleagueDiscountRepository : RepositoryBase<ColleagueDiscount, long>, IColleagueDiscountRepository
    {
        private readonly DiscountContext _discountContext;
        private readonly ShopContext _shopContext;

        public ColleagueDiscountRepository(DiscountContext discountContext,ShopContext shopContext):base(discountContext)
        {
            _discountContext = discountContext;
            _shopContext = shopContext;
        }

        public ColleagueDiscountState GetColleagueDiscountStateBy(long id)
        {
            return _discountContext.ColleagueDiscounts.Select(x => new ColleagueDiscountState
            {
                Id = x.Id,
                IsDeleted = x.IsDeleted
            }).FirstOrDefault(x => x.Id == id);
        }

        public EditColleagueDiscount GetDetails(long id)
        {
            return _discountContext.ColleagueDiscounts.Select(x => new EditColleagueDiscount
            {
                DiscountRate = x.DiscountRate,
                Id = x.Id,
                ProductId = x.ProductId
            }).FirstOrDefault(x => x.Id==id);
        }

        public List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel)
        {
            var products = _shopContext.Products.Select(x => new { x.Id, x.Name }).ToList();
            var query = _discountContext.ColleagueDiscounts.Select(x => new ColleagueDiscountViewModel
            {
                ProductId = x.ProductId,
                Id = x.Id,
                CreationDate = x.CreateionDate.ToFarsi(),
                DiscountRate = x.DiscountRate,
                IsDeleted=x.IsDeleted
            });
            if (searchModel.ProductId > 0)
                query = query.Where(x => x.ProductId == searchModel.ProductId);

            var discounts = query.OrderByDescending(x => x.Id).ToList();
            discounts.ForEach(discount => discount.ProductName = products.FirstOrDefault(x => x.Id == discount.ProductId).Name);
            return discounts;
        }
    }
}
