using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contract.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;
using System.Linq.Expressions;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class ProductCategoryRepository : RepositoryBase<ProductCategory, long>, IProductCategoryRepository
    {
        private readonly ShopContext _shopContext;
        public ProductCategoryRepository(ShopContext shopContext):base(shopContext)
        {
            _shopContext = shopContext;
        }

		public string GetCategorySlugBy(long id)
		{
            return _shopContext.ProductCategories.Select(x => new { x.Id, x.Slug }).
                FirstOrDefault(x => x.Id == id).Slug;
		}

		public EditProductCategory GetDetails(long id)
        {
            return _shopContext.ProductCategories.Select(x => new EditProductCategory
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description??"",
                Keywords = x.Keywords ?? "",
                MetaDescription = x.MetaDescription ?? "",
                //Picture = x.Picture,
                PictureAlt = x.PictureAlt ?? "",
                PictureTitle = x.PictureTitle ?? "",
                Slug = x.Slug??""
            }).FirstOrDefault(x => x.Id == id);
        }

		public List<ProductCategoryViewModel> GetProductCategories()
		{
			return _shopContext.ProductCategories.Select(x=>new ProductCategoryViewModel
            {
                Id=x.Id,
                Name=x.Name,
            }).ToList();
		}

		public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
            var query = _shopContext.ProductCategories.Include(x=>x.Products).Select(x => new ProductCategoryViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Picture = x.Picture,
                CreationDate = x.CreateionDate.ToFarsi(),
                ProductsCount = x.Products.Count
            });
            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name.Contains(searchModel.Name));
            return query.OrderByDescending(x=>x.Id).ToList();
        }
    }
}
