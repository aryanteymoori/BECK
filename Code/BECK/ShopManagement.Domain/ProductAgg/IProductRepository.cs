using _0_Framework.Domain;
using ShopManagement.Application.Contract.Product;

namespace ShopManagement.Domain.ProductAgg
{
	public interface IProductRepository:IRepository<Product,long>
	{
		List<ProductViewModel> GetProducts();
		Product GetProductWithCategories(long Id);
		EditProduct GetDetails(long id);
		List<ProductViewModel> Search(ProductSearchModel searchModel);
	}
}
