using AccountManagement.Application.Contract.Account;
using AccountManagement.Application.Contract.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Accounts.Account
{
    public class EditAccountModel : PageModel
    {
        public string Message { get; set; }
        public EditAccount Account { get; set; }
        public SelectList Roles { get; set; }
        private readonly IAccountApplication _accountApplication;
        private readonly IRoleApplication _roleApplication;

        public EditAccountModel(IAccountApplication accountApplication,IRoleApplication roleApplication)
        {
            _accountApplication = accountApplication;
            _roleApplication = roleApplication;
        }
        public void OnGet(long id, string message)
        {
            Account = _accountApplication.GetDetails(id);
            Message = message;
            Roles = new SelectList(_roleApplication.GetRoles(), "Id", "Name");
        }
        public IActionResult OnPost(EditAccount account)
        {
            if (ModelState.IsValid)
            {
                var operation = _accountApplication.Edit(account);
                if (operation.IsSuccedded)
                {
                    return RedirectToPage("Index");
                }
                else
                {
                    return RedirectToPage("EditAccount", new { account.Id, operation.Message });
                }
            }
            else
            {
                return RedirectToPage("EditAccount", new { account.Id });
            }
        }
    }
}
