namespace _0_Framework.Application
{
    public class AuthViewModel
    {
        public long Id { get; set; }
        public long RoleId {  get; set; }
        public string RoleName { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public List<int> Permisssions { get; set; }

        public AuthViewModel()
        {
        }
        public AuthViewModel(long id, long roleId, string fullName, string userName,List<int> permissions)
        {
            Id = id;
            RoleId = roleId;
            FullName = fullName;
            UserName = userName;
            Permisssions = permissions;
        }
    }
}
