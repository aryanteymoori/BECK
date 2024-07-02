using _0_Framework.Application;
using ShopManagement.Application.Contract.ProductPicture;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductPictureAgg;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ShopManagement.Application
{
    public class ProductPictureApplication : IProductPictureApplication
	{
		private readonly IProductRepository _productRepository;
		private readonly IFileUploader _fileUploader;
		private readonly IProductPictureRepository _productPictureRepository;

		public ProductPictureApplication(IProductPictureRepository productPictureRepository, IFileUploader fileUploader,IProductRepository productRepository)
		{
			_productPictureRepository = productPictureRepository;
			_fileUploader = fileUploader;
			_productRepository = productRepository;
		}

		public OperationResult Create(CreateProductPicture command)
		{
			var operation = new OperationResult();
			//if (_productPictureRepository.Exist(x => x.Picture == command.Picture && x.ProductId == command.ProductId))
			//	return operation.Faile("این رکورد قبلا ثبت شده است");

			var product = _productRepository.GetProductWithCategories(command.ProductId);
			var picturePath = $"{product.Category.Slug}/{product.Slug}";
			var picture = _fileUploader.UploadFile(command.Picture, picturePath);

			var productPicture = new ProductPicture(command.ProductId, picture,
				command.PictureAlt, command.PictureTitle);
			_productPictureRepository.Create(productPicture);
			_productPictureRepository.SaveChanges();
			return operation.Success();
		}

		public OperationResult Delete(long id)
		{
			var operation = new OperationResult();
			var productPicture = _productPictureRepository.Get(id);

			if (productPicture == null)
				return operation.Faile("رکورد مورد نظر یافت نشد");

			productPicture.Delete();
			_productPictureRepository.SaveChanges();
			return operation.Success();
		}

		public OperationResult Edit(EditProductPicture command)
		{
			var operation = new OperationResult();
			var productPicture = _productPictureRepository.Get(command.Id);

			if (productPicture == null)
				return operation.Faile("رکورد مورد نظر یافت نشد");

			//if (_productPictureRepository.Exist(x => x.Picture == command.Picture && x.ProductId == command.ProductId && x.Id != command.Id))
			//	return operation.Faile("این رکورد قبلا ثبت شده است");

			var product = _productRepository.GetProductWithCategories(command.ProductId);
			var picturePath = $"{product.Category.Slug}/{product.Slug}";
			var picture = _fileUploader.UploadFile(command.Picture, picturePath);

			productPicture.Edit(command.ProductId, picture,
				command.PictureAlt, command.PictureTitle);

			_productPictureRepository.SaveChanges();
			return operation.Success();
		}

		public EditProductPicture GetDetails(long id)
		{
			return _productPictureRepository.GetDetails(id);
		}

        public ProductPictureState GetProductPictureState(long id)
        {
            return _productPictureRepository.GetProductPictureState(id);
        }

        public OperationResult Restore(long id)
		{
			var operation = new OperationResult();
			var productPicture = _productPictureRepository.Get(id);

			if (productPicture == null)
				return operation.Faile("رکورد مورد نظر یافت نشد");

			productPicture.Restore();
			_productPictureRepository.SaveChanges();
			return operation.Success();
		}

		public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel)
		{
			return _productPictureRepository.Search(searchModel);
		}
	}
}
