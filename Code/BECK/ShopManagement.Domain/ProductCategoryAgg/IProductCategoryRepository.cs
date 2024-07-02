using _0_Framework.Domain;
using ShopManagement.Application.Contract.ProductCategory;

namespace ShopManagement.Domain.ProductCategoryAgg
{
    public interface IProductCategoryRepository:IRepository<ProductCategory,long>
    {
        List<ProductCategoryViewModel> GetProductCategories();
        EditProductCategory GetDetails(long id);
		string GetCategorySlugBy(long id);
		List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel);
    }
}
