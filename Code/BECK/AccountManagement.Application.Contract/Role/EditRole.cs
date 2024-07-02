using _0_Framework.Infrastructure;
using System.Diagnostics.CodeAnalysis;

namespace AccountManagement.Application.Contract.Role
{
	public class EditRole:CreateRole 
	{
		public long Id { get; set; }
		[MaybeNull()]
		public List<PermissionDto> MappedPermissions{get;set; }
	}
}
