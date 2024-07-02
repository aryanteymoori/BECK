using _0_Framework.Infrastructure;
using AccountManagement.Application.Contract.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Accounts.Role
{
    public class EditRoleModel : PageModel
    {
		public string Message { get; set; }

		public List<SelectListItem> Permissions =new List<SelectListItem>();
		public EditRole EditRole { get; set; }
		private readonly IRoleApplication _roleApplication;
		private readonly IEnumerable<IPermissionExposer> _exposers;

		public EditRoleModel(IRoleApplication roleApplication, IEnumerable<IPermissionExposer> exposers)
		{
			_roleApplication = roleApplication;
			_exposers = exposers;
		}

		public void OnGet(long id)
        {
			EditRole=_roleApplication.GetDetails(id);
			var permissions = new List<PermissionDto>();
			foreach (var exposer in _exposers)
			{
				var permissionsExposer = exposer.Expose();
				foreach (var (key, value) in permissionsExposer)
				{
					permissions.AddRange(value);
					var group = new SelectListGroup() { Name = key };
					foreach (var permission in value)
					{
						var item = new SelectListItem(permission.Name, permission.Code.ToString())
						{
							Group = group
						};
						if(EditRole.MappedPermissions.Any(x=>x.Code==permission.Code))
							item.Selected = true;
						Permissions.Add(item);
					}
				}
			}
		}
		public IActionResult OnPost(EditRole editRole)
		{
			if (ModelState.IsValid)
			{
				var operation = _roleApplication.Edit(editRole);
				if (operation.IsSuccedded)
				{
					return RedirectToPage("index");
				}
				else
				{
					return RedirectToPage("CreateRole", new { editRole.Id, operation.Message });
				}
			}
			else
			{
				return RedirectToPage("CreateRole", new { editRole.Id });
			}
		}
	}
}
