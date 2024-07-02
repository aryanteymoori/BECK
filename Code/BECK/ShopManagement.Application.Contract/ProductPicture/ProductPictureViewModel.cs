namespace ShopManagement.Application.Contract.ProductPicture
{
	public class ProductPictureViewModel
	{
		public long Id {  set; get; }
		public string ProductName { get; set; }
		public string Picture { get; set; }
		public string CreationDate {  get; set; }
		public bool IsDeleted { get; set; }
		public long ProductId {  get; set; }
	}

}
