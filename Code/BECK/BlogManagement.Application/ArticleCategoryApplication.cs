using _0_Framework.Application;
using _0_FrameWork.Application;
using BlogManagement.Application.Contract.ArticleCategory;
using BlogManagement.Domain.ArticleCategoryAgg;

namespace BlogManagement.Application
{
    public class ArticleCategoryApplication : IArticleCategoryApplication
    {
        private readonly IArticleCategoryRepository _articleCategoryRepository;
        private readonly IFileUploader _fileUploader;

        public ArticleCategoryApplication(IArticleCategoryRepository articleCategoryRepository, IFileUploader fileUploader)
        {
            _articleCategoryRepository = articleCategoryRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateArticleCategory command)
        {
            var operation = new OperationResult();
            if(_articleCategoryRepository.Exist(x=>x.Name==command.Name))
                return operation.Faile("این دسته مقاله قبلا ایجاد شده است");

            var slug = command.Slug.Slugify();
            var picture = _fileUploader.UploadFile(command.Picture, slug);

            var articleCategory=new ArticleCategory(command.Name, picture,
                command.Description,command.ShowOrder, slug, command.Keywords,
                command.MetaDescription,command.CanonicalAddress);
            _articleCategoryRepository.Create(articleCategory);
            _articleCategoryRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult Delete(long id)
        {
            var operation = new OperationResult();
            var articleCategory = _articleCategoryRepository.Get(id);

            if (articleCategory == null)
                return operation.Faile("رکورد مورد نظر یافت نشد");

            articleCategory.Delete();
            _articleCategoryRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult Edit(EditArticleCategory command)
        {
            var operation = new OperationResult();
            var articleCategory = _articleCategoryRepository.Get(command.Id);

            if (articleCategory == null)
                return operation.Faile("رکورد مورد نظر یافت نشد");

            if (_articleCategoryRepository.Exist(x => x.Name == command.Name && x.Id != command.Id))
                return operation.Faile("این دسته مقاله قبلا ایجاد شده است");

            var slug = command.Slug.Slugify();
            var picture = _fileUploader.UploadFile(command.Picture,slug);

            articleCategory.Edit(command.Name, picture,
                command.Description, command.ShowOrder, slug, command.Keywords,
                command.MetaDescription, command.CanonicalAddress);
            _articleCategoryRepository.SaveChanges();
            return operation.Success();
        }

        public ArticleCategoryState GetArticleCategoryStateBy(long id)
        {
            return _articleCategoryRepository.GetArticleCategoryStateBy(id);
        }

        public List<ArticleCategoryViewModel> GetArticleCatgories()
		{
			return _articleCategoryRepository.GetArticleCatgories();
		}

		public EditArticleCategory GetDetails(long id)
        {
            return _articleCategoryRepository.GetDetails(id);
        }

        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();
            var articleCategory = _articleCategoryRepository.Get(id);

            if (articleCategory == null)
                return operation.Faile("رکورد مورد نظر یافت نشد");

            articleCategory.Restore();
            _articleCategoryRepository.SaveChanges();
            return operation.Success();
        }

        public List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel)
        {
            return _articleCategoryRepository.Search(searchModel);
        }
    }
}
