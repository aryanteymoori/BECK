using AccountManagement.Application.Contract.Account;
using AccountManagement.Domain.AccountAgg;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class AccountModel : PageModel
    {
        public string LoginMessage { get; set; }
        public string RegisterMessage { get; set; }
        public Login Login { get; set; }
        public CreateAccount CreateAccount { get; set; }
        private readonly IAccountApplication _accountApplication;

        public AccountModel(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
        }

        public void OnGet(string loginMessage, string registerMessage)
        {
            LoginMessage = loginMessage;
            RegisterMessage = registerMessage;
        }
        public IActionResult OnPostLogin(Login login)
        {
            var operation = _accountApplication.Login(login);
            if (operation.IsSuccedded)
                return RedirectToPage("Index");
            return RedirectToPage("Account", new { loginMessage = operation.Message });
        }
        public IActionResult OnGetLogOut()
        {
            if (ModelState.IsValid)
            {
                _accountApplication.LogOut();
                return RedirectToPage("Index");
            }
            else
            {
                return RedirectToPage("Account");
            }
        }
        public IActionResult OnPostRegister(CreateAccount createAccount)
        {
            if (ModelState.IsValid)
            {
                var operation = _accountApplication.Create(createAccount);
                if (operation.IsSuccedded)
                {
                    var login = new Login { UserName = createAccount.UserName, Password = createAccount.Password };
                    _accountApplication.Login(login);
                    return RedirectToPage("Index");
                }
                else
                {
                    return RedirectToPage("Account", new { registermessage = operation.Message });
                }
            }
            else
            {
                return RedirectToPage("Account");
            }
        }
    }
}
