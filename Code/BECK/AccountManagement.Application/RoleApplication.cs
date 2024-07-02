using _0_Framework.Application;
using AccountManagement.Application.Contract.Role;
using AccountManagement.Domain.RoleAgg;
using System.Xml;

namespace AccountManagement.Application
{
	public class RoleApplication : IRoleApplication
	{
		private readonly IRoleRepository _roleRepository;

		public RoleApplication(IRoleRepository roleRepository)
		{
			_roleRepository = roleRepository;
		}

		public OperationResult Create(CreateRole command)
		{
			var operation = new OperationResult();

			if (_roleRepository.Exist(x => x.Name == command.Name))
				return operation.Faile("این نقش قبلا ایجاد شده است");

			var permissions=new List<Permission>();
			if (command.Permission != null)
				command.Permission.ForEach(code => permissions.Add(new Permission(code)));


			var role = new Role(command.Name,permissions);

			_roleRepository.Create(role);
			_roleRepository.SaveChanges();
			return operation.Success();
		}

		public OperationResult Edit(EditRole command)
		{
			var operation = new OperationResult();
			var role = _roleRepository.Get(command.Id);

			if (role == null)
				return operation.Faile("رکورد موردنظر یافت نشد");

			if (_roleRepository.Exist(x => x.Name == command.Name && x.Id != command.Id))
				return operation.Faile("این نقش قبلا ایجاد شده است");

			var permissions = new List<Permission>();
			if (command.Permission != null)
				command.Permission.ForEach(code => permissions.Add(new Permission(code)));

			role.Edit(command.Name, permissions);

			_roleRepository.SaveChanges();
			return operation.Success();
		}

		public EditRole GetDetails(long id)
		{
			return _roleRepository.GetDetails(id);
		}

		public string GetRoleNameBy(long roleId)
		{
			return _roleRepository.GetRoleNameBy(roleId);
		}

		public List<RoleViewModel> GetRoles()
		{
			return _roleRepository.GetRoles();
		}

		public List<RoleViewModel> Search()
		{
			return _roleRepository.Search();
		}
	}
}
