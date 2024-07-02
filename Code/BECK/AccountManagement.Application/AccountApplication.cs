using _0_Framework.Application;
using AccountManagement.Application.Contract.Account;
using AccountManagement.Domain.AccountAgg;
using AccountManagement.Domain.RoleAgg;

namespace AccountManagement.Application
{
    public class AccountApplication : IAccountApplication
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IPasswordHasherMD5 _passwordHasher;
        private readonly IAuthHelper _authHelper;
        private readonly IRoleRepository _roleRepository;

        public AccountApplication(IAccountRepository accountRepository, IPasswordHasherMD5 passwordHasher, IAuthHelper authHelper,IRoleRepository roleRepository)
        {
            _accountRepository = accountRepository;
            _passwordHasher = passwordHasher;
            _authHelper = authHelper;
            _roleRepository = roleRepository;
        }

        public OperationResult ChangePassword(ChangePassword command)
        {
            var operation = new OperationResult();
            var account = _accountRepository.Get(command.Id);

            if (account == null)
                return operation.Faile("رکورد مورد نظر یافت نشد");

            if (command.Password != command.RePassword)
                return operation.Faile("پسورد وتکرار آن برابر نمیباشند");

            account.ChangePassword(_passwordHasher.Hash(command.Password));

            _accountRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult Create(CreateAccount command)
        {
            var operation = new OperationResult();

            if (_accountRepository.Exist(x => x.UserName == command.UserName))
                return operation.Faile("این نام کاربری قبلا ثبت شده است");

            if (_accountRepository.Exist(x => x.Mobile == command.Mobile))
                return operation.Faile("این شماره موبایل قبلا ثبت شده است");

            var account = new Account(command.FullName, command.UserName, _passwordHasher.Hash(command.Password),
                command.Mobile, command.RoleId);

            _accountRepository.Create(account);
            _accountRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult Edit(EditAccount command)
        {
            var operation = new OperationResult();
            var account = _accountRepository.Get(command.Id);

            if (account == null)
                return operation.Faile("رکورد مورد نظر یافت نشد");

            if (_accountRepository.Exist(x => x.UserName == command.UserName && x.Id != command.Id))
                return operation.Faile("این نام کاربری قبلا ثبت شده است");

            if (_accountRepository.Exist(x => x.Mobile == command.Mobile && x.Id != command.Id))
                return operation.Faile("این شماره موبایل قبلا ثبت شده است");

            account.Edit(command.FullName, command.UserName,
                command.Mobile, command.RoleId);

            _accountRepository.SaveChanges();
            return operation.Success();
        }

        public EditAccount GetDetails(long id)
        {
            return _accountRepository.GetDetails(id);
        }

        public OperationResult Login(Login command)
        {
            var operation = new OperationResult();
            var account = _accountRepository.GetBy(command.UserName);
            if (account == null)
                return operation.Faile("نام کاربری پیدا نشد");
            bool result = _passwordHasher.Check(account.Password, command.Password);
            if (!result)
                return operation.Faile("کلمه عبور اشتباه است");

            var permissions = _roleRepository.Get(account.RoleId).Permissions.Select(x=>x.Code).ToList();

            var authViewModel = new AuthViewModel(account.Id, account.RoleId, account.FullName, account.UserName,permissions);
            _authHelper.SignIn(authViewModel);
            return operation.Success();
        }

        public OperationResult LogOut()
        {
            var operation= new OperationResult();
           _authHelper.SignOut();
            return operation.Success();
        }

        public List<AccountViewModel> Search(AccountSearchModel searchModel)
        {
            return _accountRepository.Search(searchModel);
        }
    }
}
