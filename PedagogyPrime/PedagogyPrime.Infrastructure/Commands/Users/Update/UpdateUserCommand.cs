namespace PedagogyPrime.Infrastructure.Commands.Users.Update
{
	using Common;
	using Core.Common;
	using Models.User;
	using PedagogyPrime.Core.Entities;
	using System.Text.Json.Serialization;

	public class UpdateUserCommand : BaseRequest<BaseResponse<UserDetails>>
	{
		[JsonIgnore]
		public Guid Id { get; set; }

		public string FirstName { get; set; }
		public string LastName { get; set; }
		public Role Role { get; set; }
	}
}