using AccountManagement.Application.Contract.Account;
using AccountManagement.Application.Contract.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Accounts.Account
{
    public class CreateAccountModel : PageModel
    {
        public string Message { get; set; }
        public SelectList Roles { get; set; }
        public CreateAccount Account { get; set; }
        private readonly IAccountApplication _accountApplication;
		private readonly IRoleApplication _roleApplication;

		public CreateAccountModel(IAccountApplication accountApplication,IRoleApplication roleApplication)
        {
            _accountApplication = accountApplication;
            _roleApplication = roleApplication;
        }

        public void OnGet(string message)
        {
            Message = message;
            Roles = new SelectList(_roleApplication.GetRoles(), "Id", "Name");
        }
        public IActionResult OnPost(CreateAccount account)
        {
            if (ModelState.IsValid)
            {
                var operation = _accountApplication.Create(account);
                if (operation.IsSuccedded)
                {
                    return RedirectToPage("Index");
                }
                else
                {
                    return RedirectToPage("CreateAccount", new { operation.Message });
                }
            }
            else
            {
                return RedirectToPage("CreateAccount");
            }
        }
    }
}
