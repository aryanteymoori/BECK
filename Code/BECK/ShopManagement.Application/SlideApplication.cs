using _0_Framework.Application;
using ShopManagement.Application.Contract.Slider;
using ShopManagement.Domain.SliderAgg;

namespace ShopManagement.Application
{
    public class SlideApplication : ISlideApplication
	{
		private readonly ISlideRepository _slideRepository;
		private readonly IFileUploader _fileUploader;

		public SlideApplication(ISlideRepository slideRepository, IFileUploader fileUploader)
		{
			_slideRepository = slideRepository;
			_fileUploader = fileUploader;
		}

		public OperationResult Create(CreateSlide command)
		{
			var operation=new OperationResult();

			var picture = _fileUploader.UploadFile(command.Picture, "Slider");

			var slide = new Slide(picture, command.PictureAlt, command.PictureTitle,
				command.Heading, command.Title, command.Text, command.BtnText,command.Link);
			_slideRepository.Create(slide);
			_slideRepository.SaveChanges();
			return operation.Success();
		}

		public OperationResult Delete(long id)
		{
			var operation = new OperationResult();
			var slide = _slideRepository.Get(id);
			if (slide == null)
				return operation.Faile("رکورد مورد نظر یافت نشد");
			slide.Delete();
			_slideRepository.SaveChanges();
			return operation.Success();
		}

		public OperationResult Edit(EditSlide command)
		{
			var operation = new OperationResult();
			var slide = _slideRepository.Get(command.Id);
			if (slide == null)
				return operation.Faile("رکورد مورد نظر یافت نشد");

			var picture = _fileUploader.UploadFile(command.Picture, "Slider");

			slide.Edit(picture, command.PictureAlt, command.PictureTitle,
				command.Heading, command.Title, command.Text, command.BtnText,command.Link);
			_slideRepository.SaveChanges();
			return operation.Success();
		}

		public EditSlide GetDetails(long id)
		{
			return _slideRepository.GetDetails(id);
		}

        public SliderState GetSliderState(long id)
        {
            return _slideRepository.GetSliderState(id);
        }

        public OperationResult Restore(long id)
		{
			var operation = new OperationResult();
			var slide = _slideRepository.Get(id);
			if (slide == null)
				return operation.Faile("رکورد مورد نظر یافت نشد");
			slide.Restore();
			_slideRepository.SaveChanges();
			return operation.Success();
		}

		public List<SlideViewModel> Search()
		{
			return _slideRepository.Search();
		}
	}
}
