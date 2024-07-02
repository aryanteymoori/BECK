using _0_Framework.Domain;
using BlogManagement.Application.Contract.ArticleCategory;

namespace BlogManagement.Domain.ArticleCategoryAgg
{
    public interface IArticleCategoryRepository:IRepository<ArticleCategory,long>
    {
        EditArticleCategory GetDetails(long id);
        string GetSlugBy(long id);
		List<ArticleCategoryViewModel> GetArticleCatgories();
		List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel);
        ArticleCategoryState GetArticleCategoryStateBy(long id);
    }
}
