namespace PedagogyPrime.Infrastructure.Commands.Authentication
{
	using Common;
	using Core.Common;
	using Models.User;

	public class LoginCommand : BaseRequest<BaseResponse<LoginResult>>
	{
		public string Username { get; set; }
		public string Password { get; set; }
	}
}