using System.Text;
using System.Security.Cryptography;

namespace _0_Framework.Application
{
    public class PasswordHasherMD5 : IPasswordHasherMD5
    {
        public string Hash(string password)
        {
            using (var md5 = MD5.Create())
            {
                var data = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
                var sb = new StringBuilder();
                foreach (var c in data)
                {
                    sb.Append(c.ToString("x2"));
                }
                return sb.ToString();
            }
        }
        public bool Check(string hash, string password)
        {
            var pass = Hash(password);
            return pass == hash;
        }
    }
}
