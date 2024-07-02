using _0_Framework.Domain;
using ShopManagement.Application.Contract.ProductPicture;

namespace ShopManagement.Domain.ProductPictureAgg
{
	public interface IProductPictureRepository:IRepository<ProductPicture,long>
	{
		EditProductPicture GetDetails(long id);
		List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel);
        ProductPictureState GetProductPictureState(long id);
    }
}
