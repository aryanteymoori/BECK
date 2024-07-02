using _0_Framework.Application;
using _01_BECKQuery.Contract.Product;
using _01_BECKQuery.Contract.ProductCategory;
using DiscountManagement.Infrastructure.EFCore;
using InventoryManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Infrastructure.EFCore;

namespace _01_BECKQuery.Query
{
    public class ProductCategoryQuery : IProductCategoryQuery
    {
        private readonly ShopContext _shopContext;
        private readonly InventoryContext _inventoryContext;
        private readonly DiscountContext _discountContext;

        public ProductCategoryQuery(ShopContext shopContext, InventoryContext inventoryContext, DiscountContext discountContext)
        {
            _shopContext = shopContext;
            _inventoryContext = inventoryContext;
            _discountContext = discountContext;
        }

        public List<ProductCategoryQueryModel> GetProductCategories()
        {
            return _shopContext.ProductCategories.Select(x => new ProductCategoryQueryModel
            {
                Name = x.Name,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt??"",
                PictureTitle = x.PictureTitle??"",
                Slug = x.Slug ?? "",
            }).ToList();
        }

        public List<ProductCategoryQueryModel> GetProductCategoriesWithProducts()
        {
            var inventory = _inventoryContext.Inventory.Select(x => new { x.ProductId, x.UnitPrice }).ToList();
            var discounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate }).ToList();
            var categories = _shopContext.ProductCategories.Include(x => x.Products).ThenInclude(x => x.Category).Select(x => new ProductCategoryQueryModel
            {
                Id = x.Id,
                Name = x.Name,
                Products = MapProducts(x.Products)
            }).ToList();

            foreach (var category in categories)
            {
                foreach (var product in category.Products)
                {
                    var productInventory = inventory.FirstOrDefault(x => x.ProductId == product.Id);
                    if (productInventory != null)
                    {
                        double price = productInventory.UnitPrice;
                        product.UnitPrice = price.ToMoney();

                        var discount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
                        if (discount != null)
                        {
                            product.DiscountRate = discount.DiscountRate;
                            product.HasDiscount = product.DiscountRate > 0;
                            if (product.HasDiscount)
                                product.PriceWithDiscount = (price - ((price * product.DiscountRate) / 100)).ToMoney();
                        }
                    }
                }
            }

            return categories;
        }
        public static List<ProductQueryModel> MapProducts(List<Product> products)
        {
            return products.Select(product => new ProductQueryModel
            {
                Id = product.Id,
                CategoryName = product.Category.Name,
                Name = product.Name,
                Picture = product.Picture,
                PictureAlt = product.PictureAlt,
                PictureTitle = product.PictureTitle,
                Slug = product.Slug,
            }).ToList();
        }

        public ProductCategoryQueryModel GetProductCategoriesWithProductsBy(string slug)
        {
            var inventory = _inventoryContext.Inventory.Select(x => new { x.ProductId, x.UnitPrice }).ToList();
            var discounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate }).ToList();
            var productCategory = _shopContext.ProductCategories
                .Include(x => x.Products)
                .ThenInclude(x => x.Category)
                .Select(x => new ProductCategoryQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Slug = x.Slug,
                    Description = x.Description,
                    Keywords=x.Keywords,
                    MetaDescription=x.MetaDescription,
                    Products = MapProducts(x.Products)
                }).FirstOrDefault(x => x.Slug == slug);

            foreach (var product in productCategory.Products)
            {
                var productInventory = inventory.FirstOrDefault(x => x.ProductId == product.Id);
                var productDiscount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
                if (productInventory != null)
                {
                    var price = productInventory.UnitPrice;
                    product.UnitPrice = price.ToMoney().ToString();
                    if (productDiscount != null)
                    {
                        product.DiscountRate = productDiscount.DiscountRate;
                        product.HasDiscount = product.DiscountRate > 0;
                        product.PriceWithDiscount = (price - ((price * productDiscount.DiscountRate) / 100)).ToMoney();
                    }
                }
            }

            return productCategory;
        }
    }
}
