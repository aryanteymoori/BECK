using BlogManagement.Application.Contract.Article;
using BlogManagement.Application.Contract.ArticleCategory;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleApplication _articleApplication;

        public ArticleController(IArticleApplication articleCategoryApplication)
        {
            _articleApplication = articleCategoryApplication;
        }

        [HttpPost("DeleteArticle/{id}")]
        public ArticleState DeleteArticle(long id)
        {
            _articleApplication.Delete(id);
            return _articleApplication.GetArticleStateBy(id);
        }


        [HttpPost("RestoreArticle/{id}")]
        public ArticleState RestoreArticle(long id)
        {
            _articleApplication.Restore(id);
            return _articleApplication.GetArticleStateBy(id);
        }
    }
}
