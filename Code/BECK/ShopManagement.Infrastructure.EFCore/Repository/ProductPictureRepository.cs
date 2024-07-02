using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contract.ProductPicture;
using ShopManagement.Domain.ProductPictureAgg;
using System.Linq.Expressions;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
	public class ProductPictureRepository : RepositoryBase<ProductPicture, long>, IProductPictureRepository
	{
		private readonly ShopContext _shopContext;

		public ProductPictureRepository(ShopContext shopContext):base(shopContext)
		{
			_shopContext = shopContext;
		}

		public EditProductPicture GetDetails(long id)
		{
			return _shopContext.ProductPictures.Select(x => new EditProductPicture
			{
				Id = x.Id,
				//Picture = x.Picture,
				PictureAlt = x.PictureAlt,
				PictureTitle = x.PictureTitle,
				ProductId = x.ProductId
			}).FirstOrDefault(x => x.Id == id);
		}

        public ProductPictureState GetProductPictureState(long id)
        {
			return _shopContext.ProductPictures.Select(x => new ProductPictureState
			{
				Id = x.Id,
				IsDeleted = x.IsDeleted
			}).FirstOrDefault(x => x.Id == id);
        }

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel)
		{
			var query = _shopContext.ProductPictures.Include(x=>x.Product).Select(x => new ProductPictureViewModel
			{
				Id = x.Id,
				Picture = x.Picture,
				CreationDate = x.CreateionDate.ToFarsi(),
				ProductName = x.Product.Name,
				ProductId=x.ProductId,
				IsDeleted=x.IsDeleted
			});
			if (searchModel.ProductId != 0)
				query = query.Where(x => x.ProductId == searchModel.ProductId);
			return query.OrderByDescending(x=>x.Id).ToList();
		}
	}
}
