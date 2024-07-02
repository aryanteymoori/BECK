using _01_BECKQuery.Contract.Article;
using CommentManagement.Application.Contract.Comment;
using CommentManagement.Infrastructure.EFCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ArticleModel : PageModel
    {
        public AddComment AddComment { get; set; }
        public List<ArticleQueryModel> Articles { get; set; }
        public ArticleQueryModel Article { get; set; }
        private readonly IArticleQuery _articleQuery;
        private readonly ICommentApplication _commentApplication;

        public ArticleModel(IArticleQuery articleQuery,ICommentApplication commentApplication)
        {
            _articleQuery = articleQuery;
            _commentApplication = commentApplication;
        }

        public void OnGet(string slug)
        {
            Article = _articleQuery.GetArticleBy(slug);
            Articles = _articleQuery.GetArticlesBy(Article.CategoryId);
        }
        public IActionResult OnPost(AddComment addComment, string slug)
        {
            if (ModelState.IsValid)
            {
                addComment.Type = CommentsType.Article;
                var operation = _commentApplication.Add(addComment);
                return RedirectToPage("Article", new { slug });
            }
            else
            {
                return RedirectToPage("Article", new { slug });
            }
        }
    }
}
