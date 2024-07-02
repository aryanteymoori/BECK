using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contract.Product;
using ShopManagement.Domain.ProductAgg;
using System.Linq.Expressions;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
	public class ProductRepository : RepositoryBase<Product, long>, IProductRepository
	{
		private readonly ShopContext _shopContext;

		public ProductRepository(ShopContext shopContext) : base(shopContext)
		{
			_shopContext = shopContext;
		}

		public EditProduct GetDetails(long id)
		{
			return _shopContext.Products.Select(x => new EditProduct
			{
				Id = x.Id,
				Name = x.Name,
				Code = x.Code,
				Description = x.Description ?? "",
				Keywords = x.Keywords ?? "",
				MetaDescription = x.MetaDescription ?? "",
				//Picture = x.Picture,
				PictureAlt = x.PictureAlt,
				PictureTitle = x.PictureTitle,
				shortDescription = x.ShortDescription ?? "",
				Slug = x.Slug,
				CategoryId = x.CategoryId
			}).FirstOrDefault(x => x.Id == id);
		}

		public List<ProductViewModel> GetProducts()
		{
			return _shopContext.Products.Select(x => new ProductViewModel
			{
				Id = x.Id,
				Name = x.Name,
			}).ToList();
		}

		public Product GetProductWithCategories(long Id)
		{
			return _shopContext.Products.Include(x => x.Category).FirstOrDefault(x => x.Id == Id);
		}

		public List<ProductViewModel> Search(ProductSearchModel searchModel)
		{
			var query = _shopContext.Products.Include(x => x.Category).Select(x => new ProductViewModel
			{
				Id = x.Id,
				Name = x.Name,
				Code = x.Code,
				Picture = x.Picture,
				CategoryName = x.Category.Name,
				CategoryId = x.CategoryId,
				CreationDate = x.CreateionDate.ToFarsi()
			}); ;

			if (!string.IsNullOrWhiteSpace(searchModel.Name))
				query = query.Where(x => x.Name.Contains(searchModel.Name));

			if (!string.IsNullOrWhiteSpace(searchModel.Code))
				query = query.Where(x => x.Code.Contains(searchModel.Code));

			if (searchModel.CategoryId != 0)
				query = query.Where(x => x.CategoryId == searchModel.CategoryId);

			return query.OrderByDescending(x => x.Id).ToList();
		}
	}
}
