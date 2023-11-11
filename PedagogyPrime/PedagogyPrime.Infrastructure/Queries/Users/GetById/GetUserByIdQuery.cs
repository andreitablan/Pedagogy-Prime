namespace PedagogyPrime.Infrastructure.Queries.Users.GetById
{
	using Common;
	using Core.Common;
	using Models.User;

	public class GetUserByIdQuery : BaseRequest<BaseResponse<UserDetails>>
	{
		public Guid Id { get; set; }

		public GetUserByIdQuery(Guid id)
		{
			Id = id;
		}
	}
}