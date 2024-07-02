using _01_BECKQuery.Contract.Article;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
	public class LatestBlogsViewComponent:ViewComponent
	{
		private readonly IArticleQuery _articleQuery;

		public LatestBlogsViewComponent(IArticleQuery articleQuery)
		{
			_articleQuery = articleQuery;
		}

		public IViewComponentResult Invoke()
		{
			return View(_articleQuery.GetLatestArticles());
		}
	}
}
