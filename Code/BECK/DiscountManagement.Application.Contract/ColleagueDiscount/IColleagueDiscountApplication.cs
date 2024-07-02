using _0_Framework.Application;

namespace DiscountManagement.Application.Contract.ColleagueDiscount
{
    public interface IColleagueDiscountApplication
    {
        OperationResult Create(CreateColleagueDiscount command);
        OperationResult Edit(EditColleagueDiscount command);
        OperationResult Delete(long id);
        OperationResult Restore(long id);
        EditColleagueDiscount GetDetails(long id);
        List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel);
        ColleagueDiscountState GetColleagueDiscountStateBy(long id);
    }
}
