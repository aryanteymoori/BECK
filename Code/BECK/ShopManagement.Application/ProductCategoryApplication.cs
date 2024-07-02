using _0_Framework.Application;
using _0_FrameWork.Application;
using ShopManagement.Application.Contract.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Application
{
    public class ProductCategoryApplication : IProductCategoryApplication
    {
        private readonly IFileUploader _fileUploader;
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductCategoryApplication(IProductCategoryRepository productCategoryRepository,IFileUploader fileUploader)
        {
            _productCategoryRepository = productCategoryRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateProductCategory command)
        {
           var operation=new OperationResult();

            if(_productCategoryRepository.Exist(x=>x.Name==command.Name))
                return operation.Faile("این دسته بندی قبلا ایجاد شده است");

            string slug =command.Slug.Slugify();

            string picture = _fileUploader.UploadFile(command.Picture, command.Slug);

            var productCategory = new ProductCategory(command.Name,command.Description,
                picture,command.PictureAlt,command.PictureTitle,command.Keywords,
                command.MetaDescription, slug);

            _productCategoryRepository.Create(productCategory);
            _productCategoryRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult Edit(EditProductCategory command)
        {
            var operatin=new OperationResult();

            var productCategory = _productCategoryRepository.Get(command.Id);

            if (productCategory == null)
                return operatin.Faile("دسته بندی مورد نظر یافت نشد");

            if (_productCategoryRepository.Exist(x => x.Name == command.Name && x.Id != command.Id))
                return operatin.Faile("این دسته بندی قبلا ایجاد شده است");

            string slug = command.Slug.Slugify();

            string picture = _fileUploader.UploadFile(command.Picture, command.Slug);

            productCategory.Edit(command.Name, command.Description,
                picture, command.PictureAlt, command.PictureTitle, command.Keywords,
                command.MetaDescription, slug);
            _productCategoryRepository.SaveChanges();
            return operatin.Success();
        }

        public EditProductCategory GetDetails(long id)
        {
            return _productCategoryRepository.GetDetails(id);
        }

		public List<ProductCategoryViewModel> GetProductCategories()
		{
			return _productCategoryRepository.GetProductCategories();
		}

		public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
            return _productCategoryRepository.Search(searchModel);
        }
    }
}
