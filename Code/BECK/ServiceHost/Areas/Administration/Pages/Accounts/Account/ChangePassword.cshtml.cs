using AccountManagement.Application.Contract.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.Accounts.Account
{
    public class ChangePasswordModel : PageModel
    {
        public string Message { get; set; }
        public ChangePassword Account { get; set; }
        private readonly IAccountApplication _accountApplication;
       
        public ChangePasswordModel(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
            Account = new ChangePassword();
        }
        public void OnGet(long id, string message,string password)
        {
            Account.Id = id;
            Message = message;
            Account.Password = password;
        }
        public IActionResult OnPost(ChangePassword account)
        {
            if (ModelState.IsValid)
            {
                var operation = _accountApplication.ChangePassword(account);
                if (operation.IsSuccedded)
                {
                    return RedirectToPage("index");
                }
                else
                {
                    return RedirectToPage("ChangePassword", new { account.Id, operation.Message , account.Password});
                }
            }
            else
            {
                return RedirectToPage("ChangePassword", new { account.Id });
            }
        }
    }
}
