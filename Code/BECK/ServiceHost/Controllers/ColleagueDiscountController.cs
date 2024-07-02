using DiscountManagement.Application.Contract.ColleagueDiscount;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColleagueDiscountController : ControllerBase
    {
        private readonly IColleagueDiscountApplication _colleagueDiscountApplication;

        public ColleagueDiscountController(IColleagueDiscountApplication colleagueDiscountApplication)
        {
            _colleagueDiscountApplication = colleagueDiscountApplication;
        }

        [HttpPost("DeleteColleagueDiscount/{id}")]
        public ColleagueDiscountState DeleteColleagueDiscount(long id)
        {
            _colleagueDiscountApplication.Delete(id);
            return _colleagueDiscountApplication.GetColleagueDiscountStateBy(id);
        }


        [HttpPost("RestoreColleagueDiscount/{id}")]
        public ColleagueDiscountState RestoreColleagueDiscount(long id)
        {
            _colleagueDiscountApplication.Restore(id);
            return _colleagueDiscountApplication.GetColleagueDiscountStateBy(id);
        }
    }
}
