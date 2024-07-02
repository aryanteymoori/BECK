using AccountManagement.Application.Contract.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.Accounts.Role
{
    public class IndexModel : PageModel
    {
        public List<RoleViewModel> Roles { get; set; }
        private readonly IRoleApplication _roleApplication;

		public IndexModel(IRoleApplication roleApplication)
		{
			_roleApplication = roleApplication;
		}

		public void OnGet()
        {
            Roles = _roleApplication.Search();
        }
    }
}
