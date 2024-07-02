using _0_Framework.Application;
using DiscountManagement.Application.Contract.ColleagueDiscount;
using DiscountManagement.Domain.ColleagueDiscountAgg;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DiscountManagement.Application
{
    public class ColleagueDiscountApplication : IColleagueDiscountApplication
    {
        private readonly IColleagueDiscountRepository _colleagueDiscountRepository;

        public ColleagueDiscountApplication(IColleagueDiscountRepository colleagueDiscountRepository)
        {
            _colleagueDiscountRepository = colleagueDiscountRepository;
        }

        public OperationResult Create(CreateColleagueDiscount command)
        {
            var operation=new OperationResult();
            if (_colleagueDiscountRepository.Exist(x => x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate))
                return operation.Faile("ایندرصد تخفیف برای این محصول قبلا تعریف شده است");
            var colleagueDiscount=new ColleagueDiscount(command.ProductId,command.DiscountRate);
            _colleagueDiscountRepository.Create(colleagueDiscount);
            _colleagueDiscountRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult Delete(long id)
        {
            var operation = new OperationResult();
            var colleagueDiscount = _colleagueDiscountRepository.Get(id);
            if (colleagueDiscount == null)
                return operation.Faile("رکورد مورد نظر یافتنشد");
            colleagueDiscount.Delete();
            _colleagueDiscountRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult Edit(EditColleagueDiscount command)
        {
            var operation = new OperationResult();
            var colleagueDiscount = _colleagueDiscountRepository.Get(command.Id);
            if (colleagueDiscount == null)
                return operation.Faile("رکورد مورد نظر یافتنشد");
            if (_colleagueDiscountRepository.Exist(x => (x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate)&& x.Id != command.Id))
                return operation.Faile("ایندرصد تخفیف برای این محصول قبلا تعریف شده است");
            colleagueDiscount.Edit(command.ProductId, command.DiscountRate);
            _colleagueDiscountRepository.SaveChanges();
            return operation.Success();
        }

        public ColleagueDiscountState GetColleagueDiscountStateBy(long id)
        {
            return _colleagueDiscountRepository.GetColleagueDiscountStateBy(id);
        }

        public EditColleagueDiscount GetDetails(long id)
        {
           return _colleagueDiscountRepository.GetDetails(id);
        }

        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();
            var colleagueDiscount = _colleagueDiscountRepository.Get(id);
            if (colleagueDiscount == null)
                return operation.Faile("رکورد مورد نظر یافتنشد");
            colleagueDiscount.Restore();
            _colleagueDiscountRepository.SaveChanges();
            return operation.Success();
        }

        public List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel)
        {
            return _colleagueDiscountRepository.Search(searchModel);
        }
    }
}
