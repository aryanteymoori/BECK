﻿using _0_Framework.Application;

namespace ShopManagement.Application.Contract.ProductPicture
{
	public interface IProductPictureApplication
	{
		OperationResult Create(CreateProductPicture command);
		OperationResult Edit(EditProductPicture command);
		OperationResult Delete(long id);
		OperationResult Restore(long id);
		EditProductPicture GetDetails(long id);
		List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel);
		ProductPictureState GetProductPictureState(long id);

	}
}
