using BlogManagement.Application.Contract.ArticleCategory;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleCategoryController : ControllerBase
    {
        private readonly IArticleCategoryApplication _articleCategoryApplication;

        public ArticleCategoryController(IArticleCategoryApplication articleCategoryApplication)
        {
            _articleCategoryApplication = articleCategoryApplication;
        }

        [HttpPost("DeleteArticleCategory/{id}")]
        public ArticleCategoryState DeleteArticleCategory(long id)
        {
            _articleCategoryApplication.Delete(id);
            return _articleCategoryApplication.GetArticleCategoryStateBy(id);
        }


        [HttpPost("RestoreArticleCategory/{id}")]
        public ArticleCategoryState RestoreArticleCategory(long id)
        {
            _articleCategoryApplication.Restore(id);
            return _articleCategoryApplication.GetArticleCategoryStateBy(id);
        }
    }
}
