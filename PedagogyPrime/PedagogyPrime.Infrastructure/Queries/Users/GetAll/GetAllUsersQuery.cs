namespace PedagogyPrime.Infrastructure.Queries.Users.GetAll
{
	using Common;
	using Models.User;
	using PedagogyPrime.Core.Common;

	public class GetAllUsersQuery : BaseRequest<BaseResponse<List<UserDetails>>>
	{
	}
}