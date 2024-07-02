using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Application.Contract.Role;
using AccountManagement.Domain.RoleAgg;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AccountManagement.Infrastructure.EFCore.Repository
{
	public class RoleRepository : RepositoryBase<Role, long>, IRoleRepository
	{
		private readonly AccountContext _context;

		public RoleRepository(AccountContext context):base(context)
		{
			_context = context;
		}

		public EditRole GetDetails(long id)
		{
			return _context.Roles.Select(x => new EditRole
			{
				Id = x.Id,
				Name = x.Name,
				MappedPermissions = MapPermissons(x.Permissions)
			}).AsNoTracking().FirstOrDefault(x => x.Id == id);
		}

		private static List<PermissionDto> MapPermissons(List<Permission> permissions)
		{
			return permissions.Select(x=>new PermissionDto(x.Code,x.Name)).ToList();
		}

		public string GetRoleNameBy(long roleId)
        {
			return _context.Roles.FirstOrDefault(x => x.Id == roleId).Name;
        }

        public List<RoleViewModel> GetRoles()
		{
			return _context.Roles.Select(x=>new RoleViewModel
			{
				Id= x.Id,
				Name= x.Name,
			}).OrderByDescending(x=>x.Id).ToList();
		}

		public List<RoleViewModel> Search()
		{
			return _context.Roles.Select(x=>new RoleViewModel 
			{
				Id = x.Id,
				Name = x.Name,
				CreationDate=x.CreateionDate.ToFarsi()
			}).OrderByDescending(x=>x.Id).ToList();
		}
	}
}
