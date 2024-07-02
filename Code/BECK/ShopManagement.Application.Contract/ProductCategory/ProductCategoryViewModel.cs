namespace ShopManagement.Application.Contract.ProductCategory
{
    public class ProductCategoryViewModel
    {
        public long Id {  set; get; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public string CreationDate {  get; set; }
        public long ProductsCount {  get; set; }
    }
}
