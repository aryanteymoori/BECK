namespace _01_BECKQuery.Contract.Article
{
	public interface IArticleQuery
	{
		List<ArticleQueryModel> GetLatestArticles();
		List<ArticleQueryModel> GetArticlesBy(long categoryId);
        ArticleQueryModel GetArticleBy(string slug);
	}
}
