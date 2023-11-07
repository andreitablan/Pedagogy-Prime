namespace PedagogyPrime.Infrastructure.Commands.Users.Create
{
	using Common;
	using PedagogyPrime.Core.Common;
	using PedagogyPrime.Core.Entities;

	public class CreateUserCommand : BaseRequest<BaseResponse<bool>>
	{
		public string Username { get; set; }
		public string Email { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Password { get; set; }
		public Role Role { get; set; }
	}
}