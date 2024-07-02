using _0_Framework.Domain;
using AccountManagement.Application.Contract.Role;

namespace AccountManagement.Domain.RoleAgg
{
	public interface IRoleRepository:IRepository<Role,long>
	{
		string GetRoleNameBy(long roleId);
		EditRole GetDetails(long id);
		List<RoleViewModel> Search();
		List<RoleViewModel> GetRoles();
	}
}
