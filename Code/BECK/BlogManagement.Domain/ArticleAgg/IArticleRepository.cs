using _0_Framework.Domain;
using BlogManagement.Application.Contract.Article;

namespace BlogManagement.Domain.ArticleAgg
{
	public interface IArticleRepository:IRepository<Article,long>
	{
		EditArticle GetDetails(long id);
		Article GetWithCategoryBy(long id);
		List<ArticleViewModel> Search(ArticleSearchModel searchModel);
        ArticleState GetArticleStateBy(long id);
    }
}
