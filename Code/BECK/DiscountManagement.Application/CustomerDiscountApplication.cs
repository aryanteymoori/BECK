using _0_Framework.Application;
using DiscountManagement.Application.Contract.CustomerDiscount;
using DiscountManagement.Domain.CustomerDiscountAgg;

namespace DiscountManagement.Application
{
    public class CustomerDiscountApplication : ICustomerDiscountApplication
    {
        private readonly ICustomerDiscountRepository _customerDiscountRepository;

        public CustomerDiscountApplication(ICustomerDiscountRepository customerDiscountRepository)
        {
            _customerDiscountRepository = customerDiscountRepository;
        }

        public OperationResult Create(CreateCustomerDiscount command)
        {
            var operation=new OperationResult();
            if (_customerDiscountRepository.Exist(x => x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate))
                return operation.Faile("این درصد تخفیف برای این محصول قبلا تعریف شده است");
            var startDate=command.StartDate.ToGeorgianDateTime();
            var endDate=command.EndDate.ToGeorgianDateTime();
            var cutomerDiscount = new CustomerDiscount(command.ProductId, command.DiscountRate,
                startDate, endDate, command.Reason);
            _customerDiscountRepository.Create(cutomerDiscount);
            _customerDiscountRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult Edit(EditCustomerDiscount command)
        {
            var operation = new OperationResult();
            var cutomerDiscount = _customerDiscountRepository.Get(command.Id);
            if (cutomerDiscount == null)
                return operation.Faile("رکورد مورد نظز یافتنشد");
            if (_customerDiscountRepository.Exist(x => (x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate) && x.Id != command.Id))
                return operation.Faile("این درصد تخفیف برای این محصول قبلا تعریف شده است");
            var startDate = command.StartDate.ToGeorgianDateTime();
            var endDate = command.EndDate.ToGeorgianDateTime();
            cutomerDiscount.Edit(command.ProductId, command.DiscountRate,
                startDate, endDate, command.Reason);
            _customerDiscountRepository.SaveChanges();
            return operation.Success();
        }

        public EditCustomerDiscount GetDetails(long id)
        {
           return _customerDiscountRepository.GetDetails(id);
        }

        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel)
        {
            return _customerDiscountRepository.Search(searchModel);
        }
    }
}
