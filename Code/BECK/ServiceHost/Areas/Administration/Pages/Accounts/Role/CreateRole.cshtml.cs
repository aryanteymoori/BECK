using _0_Framework.Infrastructure;
using AccountManagement.Application.Contract.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Accounts.Role
{
    public class CreateRoleModel : PageModel
    {
        public string Message { get; set; }
        public CreateRole CreateRole { get; set; }
        public List<SelectListItem> Permissions = new List<SelectListItem>();

        private readonly IRoleApplication _roleApplication;
        private readonly IEnumerable<IPermissionExposer> _exposers;

        public CreateRoleModel(IRoleApplication roleApplication, IEnumerable<IPermissionExposer> exposers)
        {
            _roleApplication = roleApplication;
            _exposers = exposers;
        }

        public void OnGet(string message)
        {
            var permissions = new List<PermissionDto>();
            foreach (var exposer in _exposers)
            {
                var permissionExposer = exposer.Expose();
                foreach (var (key, value) in permissionExposer)
                {
                    permissions.AddRange(value);
                    var group = new SelectListGroup() { Name = key };
                    foreach (var permission in value)
                    {
                        var item = new SelectListItem(permission.Name, permission.Code.ToString())
                        {
                            Group = group,
                        };
                        Permissions.Add(item);
                    }
                }
            }
            Message = message;
        }
        public IActionResult OnPost(CreateRole createRole)
        {
            if (ModelState.IsValid)
            {
                var operation = _roleApplication.Create(createRole);
                if (operation.IsSuccedded)
                {
                    return RedirectToPage("index");
                }
                else
                {
                    return RedirectToPage("CreateRole", new { operation.Message });
                }
            }
            else
            {
                return RedirectToPage("CreateRole");
            }
        }
    }
}
