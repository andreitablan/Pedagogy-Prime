namespace PedagogyPrime.Infrastructure.Models.User
{
	public class LoginResult
	{
		public UserDetails UserDetails { get; set; }
		public string AccessToken { get; set; }
	}
}