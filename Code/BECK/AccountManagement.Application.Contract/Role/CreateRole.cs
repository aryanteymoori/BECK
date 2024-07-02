using _0_Framework.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace AccountManagement.Application.Contract.Role
{
	public class CreateRole
	{
		[Required(ErrorMessage ="این فیلد نمیتواند خالی باشد")]
		public string Name {  get; set; }
		[MaybeNull()]
		public List<int> Permission { get; set; }
	}
}
