using _01_BECKQuery.Contract.Product;
using CommentManagement.Application.Contract.Comment;
using CommentManagement.Infrastructure.EFCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ProductModel : PageModel
    {
        public AddComment AddComment { get; set; }
        public ProductQueryModel Product { get; set; }
        private readonly IProductQuery _productQuery;
        private readonly ICommentApplication _commentApplication;
        public ProductModel(IProductQuery productQuery, ICommentApplication commentApplication)
        {
            _productQuery = productQuery;
            _commentApplication = commentApplication;
        }

        public void OnGet(string slug)
        {
            Product = _productQuery.GetProductBy(slug);
        }
        public IActionResult OnPostComment(AddComment addComment,string slug)
        {
            if (ModelState.IsValid)
            {
                addComment.Type = CommentsType.Product;
                var operation = _commentApplication.Add(addComment);
                return RedirectToPage("Product", new {slug});
            }
            else
            {
                return RedirectToPage("Product", new {slug});
            }
        }
    }
}
