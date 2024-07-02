using _01_BECKQuery.Contract.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class SearchModel : PageModel
    {
        public string SearchText {  get; set; }
        public List<ProductQueryModel> Products { get; set; }
        private readonly IProductQuery _productQuery;

        public SearchModel(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }

        public void OnGet(string value)
        {
            SearchText = value;
            Products = _productQuery.GetProductsBy(value);
        }
    }
}
