namespace _0_Framework.Application
{
    public interface IPasswordHasherMD5
    {
        public string Hash(string password);
        public bool Check(string hash, string password);
    }
}
