using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace _0_Framework.Application
{
    public class AuthHelper : IAuthHelper
	{
		private readonly IHttpContextAccessor _contextAccessor;
		public AuthHelper(IHttpContextAccessor contextAccessor)
		{
			_contextAccessor = contextAccessor;
		}

        public long CurrentAccountId()
        {
			return IsAuthenticated() ?
				long.Parse(_contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "AccountId").Value)
				: 0;
        }

        public AuthViewModel CurrentAccountInfo()
		{
			AuthViewModel result = new AuthViewModel();
			if (!IsAuthenticated())
				return result;
			var claims = _contextAccessor.HttpContext.User.Claims.ToList();
			result.Id = long.Parse(claims.FirstOrDefault(x => x.Type == "AccountId").Value);
			result.UserName = claims.FirstOrDefault(x => x.Type == "UserName").Value;
			result.FullName = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
			result.RoleId = long.Parse(claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value);
			return result;
		}

		public string CurrentAccountRole()
		{
			if (IsAuthenticated())
				return _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;
			return null;
		}

		public List<int> GetPermissions()
		{
			if (!IsAuthenticated())
				return new List<int>();

			var permision = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Permissions").Value;
			return JsonConvert.DeserializeObject<List<int>>(permision);
		}

		public bool IsAuthenticated()
		{
			return _contextAccessor.HttpContext.User.Identity.IsAuthenticated;

		}

        public void SignIn(AuthViewModel account)
		{
			var permissions = JsonConvert.SerializeObject(account.Permisssions);
			var claims = new List<Claim>
			{
				new Claim("AccountId",account.Id.ToString()),
				new Claim(ClaimTypes.Name,account.FullName),
				new Claim("UserName",account.UserName),
				new Claim(ClaimTypes.Role,account.RoleId.ToString()),
				new Claim("Permissions",permissions)
			};

			var claimIdentity = new ClaimsIdentity(claims,"Cookies");

			var authProperties = new AuthenticationProperties
			{
				ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1)
			};

			_contextAccessor.HttpContext.SignInAsync("Cookies",
				new ClaimsPrincipal(claimIdentity),
				authProperties);
		}

		public void SignOut()
		{
			_contextAccessor.HttpContext.SignOutAsync("Cookies");
		}
	}
}
