using _0_Framework.Application;
using _0_FrameWork.Application;
using ShopManagement.Application.Contract.Product;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Application
{
	public class ProductApplication : IProductApplication
	{
		private readonly IFileUploader _fileUploader;
		private readonly IProductRepository _productRepository;
		private readonly IProductCategoryRepository _productCategoryRepository;

		public ProductApplication(IProductRepository productRepository, IFileUploader fileUploader,IProductCategoryRepository productCategoryRepository)
		{
			_productRepository = productRepository;
			_fileUploader = fileUploader;
			_productCategoryRepository = productCategoryRepository;
		}

		public OperationResult Create(CreateProduct command)
		{
			var operation = new OperationResult();
			if(_productRepository.Exist(x=>x.Name==command.Name))
				return operation.Faile("محصولی با این نام قبلا ایجاد شده است");
			var slug=command.Slug.Slugify();

			var picturePath = $"{_productCategoryRepository.GetCategorySlugBy(command.CategoryId)}/{command.Slug}";
			var picture = _fileUploader.UploadFile(command.Picture, picturePath);

			var product=new Product(command.Name,command.Code,
				command.shortDescription,command.Description, picture,
				command.PictureAlt,command.PictureTitle,command.CategoryId,
				slug, command.Keywords,command.MetaDescription);
			_productRepository.Create(product);
			_productRepository.SaveChanges();
			return operation.Success();
		}

		public OperationResult Edit(EditProduct command)
		{
			var operation=new OperationResult();
			var product = _productRepository.GetProductWithCategories(command.Id);
			if (product == null)
				return operation.Faile("محصول مورد نظر یافت نشد");
			if (_productRepository.Exist(x => x.Name == command.Name && x.Id != command.Id))
				return operation.Faile("محصولی با این نام قبلا ایجاد شده است");
			var slug=command.Slug.Slugify();

			var picturePath = $"{product.Category.Slug}/{command.Slug}";
			var picture = _fileUploader.UploadFile(command.Picture, picturePath);

			product.Edit(command.Name, command.Code,
				command.shortDescription, command.Description, picture,
				command.PictureAlt, command.PictureTitle, command.CategoryId,
				slug, command.Keywords, command.MetaDescription);
			_productRepository.SaveChanges();
			return operation.Success();
		}

		public EditProduct GetDetails(long id)
		{
			return _productRepository.GetDetails(id);
		}

		public List<ProductViewModel> GetProducts()
		{
			return _productRepository.GetProducts();
		}



		public List<ProductViewModel> Search(ProductSearchModel searchModel)
		{
			return _productRepository.Search(searchModel);
		}
	}
}
