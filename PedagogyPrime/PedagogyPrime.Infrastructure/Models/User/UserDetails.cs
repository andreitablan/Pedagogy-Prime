using PedagogyPrime.Core.Entities;

namespace PedagogyPrime.Infrastructure.Models.User
{
	public class UserDetails
	{
		public string Username { get; set; }
		public string Email { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public Role Role { get; set; }
	}
}