using _0_Framework.Application;

namespace BlogManagement.Application.Contract.Article
{
	public interface IArticleApplication
	{
		OperationResult Create(CreateArticle command);
		OperationResult Edit(EditArticle command);
		OperationResult Delete(long id);
		OperationResult Restore(long id);
		EditArticle GetDetails(long id);
		List<ArticleViewModel> Search(ArticleSearchModel searchModel);
		ArticleState GetArticleStateBy(long id);
	}
}
