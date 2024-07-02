using ShopManagement.Application.Contract.Order;

namespace _01_BECKQuery.Contract.Product
{
    public interface IProductQuery
    {
        List<ProductQueryModel> GetProducts();
        List<ProductQueryModel> GetProductsBy(string value);
        ProductQueryModel GetProductBy(string slug);
        List<CartItem> CheckInventoryStatus(List<CartItem> cartItems);
    }
}
