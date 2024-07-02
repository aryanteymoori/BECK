using _0_Framework.Application;
using _0_FrameWork.Application;
using BlogManagement.Application.Contract.Article;
using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Domain.ArticleCategoryAgg;

namespace BlogManagement.Application
{
	public class ArticleApplication : IArticleApplication
	{
		private readonly IArticleCategoryRepository _articleCategoryRepository;
		private readonly IArticleRepository _articleRepository;
		private readonly IFileUploader _fileUploader;

		public ArticleApplication(IArticleRepository articleRepository, IFileUploader fileUploader, IArticleCategoryRepository articleCategoryRepository)
		{
			_articleCategoryRepository = articleCategoryRepository;
			_articleRepository = articleRepository;
			_fileUploader = fileUploader;
		}

		public OperationResult Create(CreateArticle command)
		{
			var operation = new OperationResult();
			if (_articleRepository.Exist(x => x.Title == command.Title))
				return operation.Faile("مقاله با این نام قبلا ایجاد شده است");

			var slug = command.Slug.Slugify();
			var picture = _fileUploader.UploadFile(command.Picture, $"{_articleCategoryRepository.GetSlugBy(command.CategoryId)}/{command.Slug}");

			var article = new Article(command.Title, command.ShortDescription,
				command.Description, picture, command.PictureAlt, command.PictureTitle,
				slug, command.Keywords, command.MetaDescription, command.CanonicalAddress,
				command.CategoryId);

			_articleRepository.Create(article);
			_articleRepository.SaveChanges();
			return operation.Success();
		}

		public OperationResult Delete(long id)
		{
			var operation = new OperationResult();
			var article = _articleRepository.GetWithCategoryBy(id);

			if (article == null)
				return operation.Faile("رکورد مورد نظر یافت نشد");

			article.Delete();

			_articleRepository.SaveChanges();
			return operation.Success();
		}

		public OperationResult Edit(EditArticle command)
		{
			var operation = new OperationResult();
			var article = _articleRepository.GetWithCategoryBy(command.Id);

			if (article == null)
				return operation.Faile("رکورد مورد نظر یافت نشد");

			if (_articleRepository.Exist(x => x.Title == command.Title && x.Id != command.Id))
				return operation.Faile("مقاله با این نام قبلا ایجاد شده است");

			var slug = command.Slug.Slugify();
			var picture = _fileUploader.UploadFile(command.Picture, $"{article.ArticleCategory.Slug}/{command.Slug}");

			 article.Edit(command.Title, command.ShortDescription,
				command.Description, picture, command.PictureAlt, command.PictureTitle,
				slug, command.Keywords, command.MetaDescription, command.CanonicalAddress,
				command.CategoryId);

			_articleRepository.SaveChanges();
			return operation.Success();
		}

        public ArticleState GetArticleStateBy(long id)
        {
            return _articleRepository.GetArticleStateBy(id);
        }

        public EditArticle GetDetails(long id)
		{
			return _articleRepository.GetDetails(id);
		}

		public OperationResult Restore(long id)
		{
			var operation = new OperationResult();
			var article = _articleRepository.GetWithCategoryBy(id);

			if (article == null)
				return operation.Faile("رکورد مورد نظر یافت نشد");

			article.Restore();

			_articleRepository.SaveChanges();
			return operation.Success();
		}

		public List<ArticleViewModel> Search(ArticleSearchModel searchModel)
		{
			return _articleRepository.Search(searchModel);
		}
	}
}
