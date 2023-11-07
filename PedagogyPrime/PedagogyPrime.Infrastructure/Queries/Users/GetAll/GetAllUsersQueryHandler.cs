namespace PedagogyPrime.Infrastructure.Queries.Users.GetAll
{
	using Common;
	using Core.Common;
	using Core.Entities;
	using Core.IRepositories;
	using MediatR;
	using Models.User;

	public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, BaseResponse<List<UserDetails>>>
	{
		private readonly IUserRepository userRepository;

		public GetAllUsersQueryHandler(IUserRepository userRepository)
		{
			this.userRepository = userRepository;
		}

		public async Task<BaseResponse<List<UserDetails>>> Handle(
			GetAllUsersQuery request,
			CancellationToken cancellationToken
		)
		{
			try
			{
				var users = await userRepository.GetAll();

				var usersDetails = users.Select(GenericMapper<User, UserDetails>.Map).ToList();

				return BaseResponse<List<UserDetails>>.Ok(usersDetails);
			}
			catch(Exception e)
			{
				return BaseResponse<List<UserDetails>>.BadRequest(
					new List<string>
					{
						e.Message
					}
				);
			}
		}
	}
}