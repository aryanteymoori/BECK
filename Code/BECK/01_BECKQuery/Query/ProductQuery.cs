using _0_Framework.Application;
using _01_BECKQuery.Contract.Comment;
using _01_BECKQuery.Contract.Product;
using CommentManagement.Infrastructure.EFCore;
using DiscountManagement.Infrastructure.EFCore;
using InventoryManagement.Domain.InventoryAgg;
using InventoryManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contract.Order;
using ShopManagement.Domain.ProductPictureAgg;
using ShopManagement.Infrastructure.EFCore;

namespace _01_BECKQuery.Query
{
    public class ProductQuery : IProductQuery
    {
        private readonly ShopContext _shopContext;
        private readonly InventoryContext _inventoryContext;
        private readonly DiscountContext _discountContext;
        private readonly CommentContext _commentContext;

        public ProductQuery(ShopContext shopContext, InventoryContext inventoryContext, DiscountContext discountContext, CommentContext commentContext)
        {
            _shopContext = shopContext;
            _inventoryContext = inventoryContext;
            _discountContext = discountContext;
            _commentContext = commentContext;
        }

        public ProductQueryModel GetProductBy(string slug)
        {
            var discounts = _discountContext.CustomerDiscounts
               .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
               .Select(x => new { x.ProductId, x.DiscountRate }).ToList();
            var invenotry = _inventoryContext.Inventory.Select(x => new { x.ProductId, x.UnitPrice }).ToList();

            var product = _shopContext.Products
                .Include(x => x.Category)
                .Include(x => x.ProductPictures)
                .Select(x => new ProductQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Slug = x.Slug,
                    CategoryName = x.Category.Name,
                    Code = x.Code,
                    Description = x.Description,
                    Keywords = x.Keywords,
                    MetaDescription = x.MetaDescription,
                    ShortDescription = x.ShortDescription,
                    CategorySlug = x.Category.Slug,
                    ProductPictures = MapProductPictures(x.ProductPictures),
                }).FirstOrDefault(x => x.Slug == slug);

            var productDiscount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
            var productInventory = invenotry.FirstOrDefault(x => x.ProductId == product.Id);

            if (productInventory != null)
            {
                var price = productInventory.UnitPrice;
                product.UnitPrice = productInventory.UnitPrice.ToMoney();
                product.DoubleUnitPrice = productInventory.UnitPrice;

                if (productDiscount != null)
                {
                    product.DiscountRate = productDiscount.DiscountRate;
                    product.HasDiscount = productDiscount.DiscountRate > 0;
                    product.PriceWithDiscount = (price - (price * productDiscount.DiscountRate) / 100).ToMoney();
                }
            }

            product.Comments = _commentContext.Comments
                 .Where(x => x.Type == CommentsType.Product)
                 .Where(x => !x.IsCanceled)
                 .Where(x => x.IsConfirmed)
                 .Where(x => x.OwnerRecordId == product.Id)
                 .Select(x => new CommentQueryModel
                 {
                     Id = x.Id,
                     Name = x.Name,
                     Message = x.Message,
                     CreationDate = x.CreateionDate.ToFarsi()
                 }).OrderByDescending(x => x.Id).ToList();

            return product;
        }
        public static List<ProductPictureQueryModel> MapProductPictures(List<ProductPicture> pictures)
        {
            return pictures.Where(x => !x.IsDeleted).Select(x => new ProductPictureQueryModel
            {
                ProductId = x.ProductId,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
            }).ToList();
        }

        public List<ProductQueryModel> GetProducts()
        {
            var discounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate }).ToList();
            var invenotry = _inventoryContext.Inventory.Select(x => new { x.ProductId, x.UnitPrice }).ToList();
            var products = _shopContext.Products.Include(x => x.Category).Select(x => new ProductQueryModel
            {
                Id = x.Id,
                Name = x.Name,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug,
                CategoryName = x.Category.Name,
            }).OrderByDescending(x => x.Id).Take(5).ToList();
            foreach (var product in products)
            {
                var productDiscount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
                var productInventory = invenotry.FirstOrDefault(x => x.ProductId == product.Id);
                if (productInventory != null)
                {
                    var price = productInventory.UnitPrice;
                    product.UnitPrice = productInventory.UnitPrice.ToMoney();

                    if (productDiscount != null)
                    {
                        product.DiscountRate = productDiscount.DiscountRate;
                        product.HasDiscount = productDiscount.DiscountRate > 0;
                        product.PriceWithDiscount = (price - (price * productDiscount.DiscountRate) / 100).ToMoney();
                    }
                }


            }
            return products;
        }

        public List<ProductQueryModel> GetProductsBy(string value)
        {
            var discounts = _discountContext.CustomerDiscounts
                            .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                            .Select(x => new { x.ProductId, x.DiscountRate }).ToList();
            var invenotry = _inventoryContext.Inventory.Select(x => new { x.ProductId, x.UnitPrice }).ToList();
            var products = _shopContext.Products
                .Where(x => x.Name.Contains(value))
                .Include(x => x.Category)
                .Select(x => new ProductQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Slug = x.Slug,
                    CategoryName = x.Category.Name,
                }).OrderByDescending(x => x.Id).ToList();
            foreach (var product in products)
            {
                var productDiscount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
                var productInventory = invenotry.FirstOrDefault(x => x.ProductId == product.Id);
                if (productInventory != null)
                {
                    var price = productInventory.UnitPrice;
                    product.UnitPrice = productInventory.UnitPrice.ToMoney();

                    if (productDiscount != null)
                    {
                        product.DiscountRate = productDiscount.DiscountRate;
                        product.HasDiscount = productDiscount.DiscountRate > 0;
                        product.PriceWithDiscount = (price - (price * productDiscount.DiscountRate) / 100).ToMoney();
                    }
                }


            }

            return products;
        }

        public List<CartItem> CheckInventoryStatus(List<CartItem> cartItems)
        {
            var productInventory = _inventoryContext.Inventory.ToList();
            if (cartItems != null)
            {
                foreach (var item in cartItems.Where(cartItem=>productInventory.Any(p=>p.ProductId== cartItem.Id && p.IsInStock)))
                {
                    var inventory = productInventory.FirstOrDefault(x => x.ProductId == item.Id);
                    if (inventory != null)
                    {
                        item.IsInStock = inventory.CalculateCurrentCount() >= item.Count;
                    }
                    //if (productInventory.Any(x => x.ProductId == item.Id && x.IsInStock))
                    //    item.IsInStock = true;
                }
            }
            return cartItems;
        }
    }
}
